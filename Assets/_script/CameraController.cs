using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		targetPos = startPos;
		startSize = GetComponent<Camera>().orthographicSize;
		targetSize = startSize;
		offset = startPos - GetWorldPosAtViewportPoint(0.5f, 0.5f);	
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
}
