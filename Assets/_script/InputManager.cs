using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public GameObject c;
	// Allows different modes for input
	public enum context{ build, select, destroy };
	private context currentContext;
	// Hold camera's initial position
	private Vector3 cameraPos;

	// Use this for initialization
	void Start () {
		//cameraPos = 
		currentContext = context.select;
	}
	
	// Update is called once per frame
	void Update () {
		// Not sure if this is the best way to write this
		// Context Switcher/////////////////////////////////////////////////
		if(Input.GetKeyDown(KeyCode.S)){ currentContext = context.select; }
		else if(Input.GetKeyDown(KeyCode.B)){ currentContext = context.build; }
		else if(Input.GetKeyDown(KeyCode.D)){ currentContext = context.destroy; }
		// Build mode //////////////////////////////////////////////////////
		if(currentContext == context.build){
			// Build house
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
			if(Input.GetKeyDown(KeyCode.RightArrow)){
				GetComponent<HomeManager>().nextHome();
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow)){
				GetComponent<HomeManager>().prevHome();
			}
		}
		// Select Mode /////////////////////////////////////////////////////
		else if(currentContext == context.select){
			// Focus/Unfocus
			if(Input.GetMouseButtonDown(0)){
				if(isCameraFocused()){ unfocus(); }
				else{
					Ray mRay = 
						c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
					RaycastHit info;
					if(Physics.Raycast(mRay, out info, 100)){
						Transform child;
						if(info.collider.gameObject.FindChildwithTag("Home", out child)){
							focusOn(child);
						}
					}
				}
			}
		}
		// Destory Mode ////////////////////////////////////////////////////
		else if(currentContext == context.destroy){
			// Destroy house
			if(Input.GetMouseButtonDown(0)){
				Ray mRay = 
					c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				if(Physics.Raycast(mRay, out info, 100)){
					Transform child;
					if(info.collider.gameObject.FindChildwithTag("Home", out child)){
						if(GetComponent<HomeManager>().DestroyHome(info.collider.gameObject) &&
						isCameraFocused()){
							unfocus();
						}
					}
				}
			}

		}
	}

	// Helper functions to simplify camera focus
	private bool isCameraFocused(){
		return c.GetComponent<CameraController>().isFocused();
	}

	private void focusOn(Transform child){
		c.GetComponent<CameraController>().focusOn(child);
	}

	private void unfocus(){
		c.GetComponent<CameraController>().unfocus();
	}

	public context getCurrentContext(){
		return currentContext;
	} 
	void OnGUI(){
		// Context indicator
		string text = "";
		if(currentContext == context.build){ text = "Build"; }
		else if(currentContext == context.select){text = "Select"; }
		else if(currentContext == context.destroy){text = "Destroy"; }
		GUI.Box(new Rect(0, 0, 100, 25), text);
	}
}
