using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
	public GameObject GridSpace;
	public GameObject c;
	public int numSpaces = 6;
	public int spacesPerRow = 3;
	private GameObject[] GridSpaces;

	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {
		// Hilight gridspace on mouseover
		Ray mRay = 
			c.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		RaycastHit info;
		string currMoving = (-1).ToString();
		if(Physics.Raycast(mRay, out info, 100)){
			if(info.collider.tag == "Grid"){
				if(info.collider.transform.position.y != 2){
					// Record name
					currMoving = info.collider.name;
					// Move Gridspace up
					Vector3 currPos = info.collider.transform.position;
					 currPos = Vector3.Lerp(
						currPos, 
						new Vector3(currPos.x, 2, currPos.z),
						Time.deltaTime);
					info.collider.transform.position = currPos;
				}
			}
		}
		// Push all other platforms to original position
		foreach(var space in GridSpaces){
			if(space.name != currMoving &&
			   space.transform.position.y != 0){
				   Vector3 currPos = space.transform.position;
				   currPos = Vector3.Lerp(
					   currPos,
					   new Vector3(currPos.x, 0, currPos.z),
					   Time.deltaTime);
					   space.transform.position = currPos;
			   }
		}

	}
}
