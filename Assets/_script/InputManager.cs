using FindChildwithTag;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public GameObject c;
	// Allows different modes for input
	public enum context{ build, select, destroy, tenant, focused };
	private context currentContext;
	// Hold camera's initial position
	private Vector3 cameraPos;
	// currently focused home, null if not focused
	private GameObject focusedHome;

	void Start () {
		focusedHome = null; 
		currentContext = context.select;
	}
	
	// Update is called once per frame
	void Update () {
		// Not sure if this is the best way to write this
		// Context Switcher/////////////////////////////////////////////////
		if(isCameraFocused()){ currentContext = context.focused; }
		else if(Input.GetKeyDown(KeyCode.S)){ currentContext = context.select;  }
		else if(Input.GetKeyDown(KeyCode.B)){ currentContext = context.build;   }
		else if(Input.GetKeyDown(KeyCode.D)){ currentContext = context.destroy; }
		else if(Input.GetKeyDown(KeyCode.T)){ currentContext = context.tenant;  }
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
		// Tenant Mode /////////////////////////////////////////////////////
		// For testing
		else if(currentContext == context.tenant){
			// Add Tenant
			if(Input.GetMouseButtonDown(0)){
				Ray mRay = 
					c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				if(Physics.Raycast(mRay, out info, 100)){
					Transform child;
					if(info.collider.gameObject.FindChildwithTag("Home", out child)){
						   if(!GetComponent<HomeManager>().registerTenant("Test name", 99, child.gameObject)){
							Debug.Log("Can't create Tenant");
						}
					}
				}
			}

		}
		// Focused Mode ////////////////////////////////////////////////////
		else if(currentContext == context.focused){

		}
	}

	// Helper functions to simplify camera focus
	private bool isCameraFocused(){
		return c.GetComponent<CameraController>().isFocused();
	}

	private void focusOn(Transform child){
		c.GetComponent<CameraController>().focusOn(child);
		focusedHome = child.gameObject;
	}

	private void unfocus(){
		c.GetComponent<CameraController>().unfocus();
		focusedHome = null;
	}

	public context getCurrentContext(){
		return currentContext;
	}

	void OnGUI(){
		// Context indicator
		string text = "";
		if(currentContext == context.build){ text = "Build"; }
		else if(currentContext == context.select){ text = "Select";   }
		else if(currentContext == context.destroy){ text = "Destroy"; }
		else if(currentContext == context.tenant){ text = "Tenant";   }
		else if(currentContext == context.focused){ text = "Focused"; }
		GUI.Box(new Rect(0, 0, 100, 25), text);

		if(isCameraFocused()){
			// Display home stats/Tenant on focus
			Vector3 b = focusedHome.GetComponent<Renderer>().bounds.max;
			Vector3 offset = c.GetComponent<Camera>().WorldToScreenPoint(b);
			//Debug.Log(offset);
			
			//GUILayout.BeginArea(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100));
			GUILayout.BeginArea(new Rect(offset.x + 100, Screen.height/2 - 50, 200, 100));
			Tenant t = focusedHome.GetComponent<Tenant>();
			GUILayout.Button("Name: " + t.TenantName());
			GUILayout.Button("Rent: " + t.cash());
			if(GUILayout.Button("Exit")){
				unfocus();
				currentContext = context.select;
			}
			GUILayout.EndArea();		
		}
		else{
			// Controls at bottom of main screen
			GUILayout.BeginArea(new Rect(Screen.width/2 - 150, Screen.height - 50, 300, 100));
			GUILayout.Button("Build : B, Destroy: D, Select: S");
			GUILayout.EndArea();
		}
	}
}
