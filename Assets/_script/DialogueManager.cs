using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public GameObject canvas;
	public GameObject textBoxPrefab;

	private GameObject textBox;
	private GameObject textName;
	private GameObject textContent;

	void Start () {
		// Instantiate textbox and get ready to receive text
		textBox = GameObject.Instantiate(textBoxPrefab, canvas.transform, false);
		// Gather the name text box and the content text box
		// There's probably a better way to do the textname getting

		GameObject namePanel;
		if(!tryGetChildwithTag("NamePanel", textBox, out namePanel)){
			throw new Exception("Can't find child with tag 'NamePanel'");
		}
		if(!tryGetChildwithTag("TextName", namePanel, out textName)){
			throw new Exception("Can't find child with tag 'TextName'");
		}
		
		if(!tryGetChildwithTag("TextContent", textBox, out textContent)){
			throw new Exception("Can't find child with tag 'TextContent'");
		}

		
	}


	private void setText(string t){
		textContent.GetComponent<Text>().text = t;
	}
	private void setName(string t){
		textName.GetComponent<Text>().text = t;
	}
	private bool tryGetChildwithTag(string t, GameObject parent, out GameObject toRet){
		toRet = null;
		foreach(Transform child in parent.GetComponentInChildren<Transform>()){
			if(child.gameObject.tag == t){ 
				toRet = child.gameObject;
				return true;
			}
		}

		return false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
