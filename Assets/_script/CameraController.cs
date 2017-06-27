using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HomeType;

public class CameraController : MonoBehaviour {
	public float ZoomTarget = 3f;
	private Vector3 startPos;
	private Vector3 offset;
	private Vector3 targetPos;
	private Vector3 camSmoothDampV;
	private float startSize;
	private float targetSize;
	private float zoomSmoothDampV;
	private bool focused;
	// holds world reference for debug gui
	private GameObject world;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		targetPos = startPos;
		startSize = GetComponent<Camera>().orthographicSize;
		targetSize = startSize;
		offset = startPos - GetWorldPosAtViewportPoint(0.5f, 0.5f);
		world = GameObject.FindWithTag("World");
	}

	void FixedUpdate () {
		// Move the camera smoothly to the target position
        transform.position = Vector3.SmoothDamp(
            transform.position, targetPos, ref camSmoothDampV, 0.5f);
		GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(
			GetComponent<Camera>().orthographicSize, targetSize, ref zoomSmoothDampV, 0.5f);
		
	}

	public void focusOn(Transform subject){
		// set target
		if(!focused){
			targetPos = subject.position + offset;
			focused = true;
			targetSize = ZoomTarget;
		}
	}

	public void unfocus(){
		if(focused){
			targetPos = startPos;
			focused = false;
			targetSize = startSize;
		}
	}

	public bool isFocused(){ return focused; }
	// Adapted from JoeStrout
	private Vector3 GetWorldPosAtViewportPoint(float vx, float vy) {
        Ray worldRay = GetComponent<Camera>().ViewportPointToRay(new Vector3(vx, vy, 0));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distanceToGround;
        groundPlane.Raycast(worldRay, out distanceToGround);
        return worldRay.GetPoint(distanceToGround);
    }

	private HomeEnum getCurrentHome(){
		return world.GetComponent<HomeManager>().getCurrentHome();
	}

	void OnGUI(){
		// Giant mess of debug ui
		if(SceneManager.GetActiveScene().name == "main"){
			InputManager.context currentContext = world.GetComponent<InputManager>().getCurrentContext();
			// Home indicator
			if(currentContext == InputManager.context.build){
				string homeText = "";
				if(getCurrentHome() == HomeEnum.A){ homeText = "Home: A"; }
				else if(getCurrentHome() == HomeEnum.B){ homeText = "Home: B"; }
				GUI.Box(new Rect(0, 54, 100, 25), homeText);
			}
			// Context indicator
			string contextText = "";
			if(currentContext == InputManager.context.build){ contextText = "Build"; }
			else if(currentContext == InputManager.context.select) { contextText = "Select";  }
			else if(currentContext == InputManager.context.destroy){ contextText = "Destroy"; }
			else if(currentContext == InputManager.context.tenant) { contextText = "Tenant";  }
			else if(currentContext == InputManager.context.focused){ contextText = "Focused"; }
			GUI.Box(new Rect(0, 0, 100, 25), contextText);
			// Cash Indicator
			string cashText = world.GetComponent<EconomyManager>().getCash().ToString();
			GUI.Box(new Rect(0, 27, 100, 25), cashText);
		}
		// Scene Switcher
		if(GUI.Button(new Rect(Screen.width - 102 ,2, 100, 100), "Load Scene")){
			if(SceneManager.GetActiveScene().name == "main"){
				world.GetComponent<HomeManager>().saveHomes();
				world.GetComponent<EconomyManager>().saveCash();
				SceneManager.LoadScene("dialogue");
			}
			else{
				SceneManager.LoadScene("main");
			}
		}
		string sceneText = "Scene" + SceneManager.GetActiveScene().name;
		GUI.Label(new Rect(Screen.width - 102, 104, 100, 100), sceneText);
	}
}
