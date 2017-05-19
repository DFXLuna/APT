using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public GameObject c;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Ray mRay = 
				c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit info;
			if(Physics.Raycast(mRay, out info, 100)){
				if(info.collider.tag == "Grid"){
					GetComponent<HomeManager>().SpawnHome(info.collider.gameObject);
				}
			}
		}
		
	}
}
