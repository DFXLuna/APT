using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
	// Lerp adapted from BlueRaja's correct lerp
	public GameObject GridSpace;
	public GameObject c;
	public int numSpaces = 6;
	public int spacesPerRow = 3;
	public float lerpmax = .5f;
	public float lerptime = .2f;
	private GameObject[] GridSpaces;
	// Maps GridSpace name to (startY, endY, start time) for lerp
	private Dictionary<string, Vector3> lerptionary;
	// Use this for initialization
	void Start () {
		lerptionary = new Dictionary<string, Vector3>();
		// Create Grid
		GridSpaces = new GameObject[numSpaces];
		Vector2 xy;
		Vector3 pos;
		for(int i = 0; i < numSpaces; i++){
			xy = new Vector2((i % spacesPerRow) * 8.5f,
							(i / spacesPerRow)  * 6.5f);
			pos = new Vector3(xy.x, 0, xy.y);
			GridSpaces[i] = Instantiate(GridSpace, pos, Quaternion.identity);
			GridSpaces[i].name = i.ToString();
		}
	}
	
	void Update () {
		// Push gridspace on mouseover
		Ray mRay = 
			c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		RaycastHit info;
		string currMoving = (-1).ToString();
		if(Physics.Raycast(mRay, out info, 100)){
			if(info.collider.tag == "Grid"){
				startLerp(info.collider.gameObject, lerpmax);
				currMoving = info.collider.name;
			}
		}
		// Push all other platforms to original position
		foreach(var space in GridSpaces){
			if(space.name != currMoving && 
			(isLerpingUp(space.name)) || space.transform.position.y == lerpmax){
				startLerp(space, 0);
			}
		}
	}

	void FixedUpdate(){
		// Apply lerp to all lerping objects
		List<string> keys = new List<string>(lerptionary.Keys);
		foreach(string k in keys){
			float timeSinceStarted = Time.time - lerptionary[k].z;
			float percentComplete = timeSinceStarted / lerptime;
			int i = getIndex(k);
			Vector3 currPos = GridSpaces[i].transform.position;
			GridSpaces[i].transform.position = Vector3.Lerp(
				new Vector3(currPos.x, lerptionary[k].x, currPos.z),
				new Vector3(currPos.x, lerptionary[k].y, currPos.z),
				percentComplete);
			if(percentComplete >= 1.0f){
				lerptionary.Remove(k);
			}
		}
	}

	void startLerp(GameObject GridSpace, float targetY){
		lerptionary[GridSpace.name] = 
			new Vector3(GridSpace.transform.position.y,targetY, Time.time);
	}

	bool isLerping(string GridSpaceName){
		Vector3 a;
		if(lerptionary.TryGetValue(GridSpaceName, out a)){
			return true;
		}
		else{
			return false;
		}
	}

	bool isLerpingUp(string GridSpaceName){
		Vector3 a;
		if(lerptionary.TryGetValue(GridSpaceName, out a)){
			return (a.y == lerpmax);
		}
		else{
			return false;
		}
	}

	public int getIndex(GameObject GridSpace){
		int ret = 0;
		if(Int32.TryParse(GridSpace.name, out ret)){
			return ret;
		}
		else{
			return -1;
		}
	}

	public int getIndex(string GridSpaceName){
		int ret = 0;
		if(Int32.TryParse(GridSpaceName, out ret)){
			return ret;
		}
		else{
			return -1;
		}		
	}

	public GameObject getGridSpace(string index){
		return GridSpaces[getIndex(index)];
	}
}
