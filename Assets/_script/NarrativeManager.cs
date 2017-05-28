using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NarrativeTypes;
using NarrativeReader;


public class NarrativeManager : MonoBehaviour {
	// Maps condition to associated narrative 
	private Dictionary<condition, narrative> narratives;
	private Reader reader;

	void Start () {
		narratives = new Dictionary<condition, narrative>();
		reader = new Reader("Assets/_script/test.slum");
		// FOR TESTING
		NarrativeEvent test = reader.readNextScene();
		while(!test.isEnd()){
			Debug.Log(test.nextLine());
		}
		test = reader.readNextScene();
		while(!test.isEnd()){
			Debug.Log(test.nextLine());
		}
	}
	
	void Update () {
		// This probably doesn't need to be called every frame
		List<condition> keys = new List<condition>(narratives.Keys);
		foreach(var k in keys){
			if(k()){
				callNarrative(narratives[k]);
				narratives.Remove(k);
			}
		}
		
	}

	public void registerNarrativeEvent(condition c, narrative n){
		narratives[c] = n;
	}

	// Allow for setup prior to n()
	private void callNarrative(narrative n){
		n();
	}
}
