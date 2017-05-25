using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour {
	public GameObject HomeA;
	public GameObject HomeB;
	// Indices match Grid array in GridManager
	private GameObject[] homes;
	private enum home{ A, B };
	private int numHomeTypes;
	private home currentHome;
	// Maps a home enum to a y axis offset
	private Dictionary<home, Vector3> homeOffsets;

	void Start(){
		// Home Array
		int numSpaces = GetComponent<GridManager>().numSpaces;
		homes = new GameObject[numSpaces];
		// Home Selection
		currentHome = home.A;
		numHomeTypes = Enum.GetValues(typeof(home)).Length;
		// Offsets
		homeOffsets = new Dictionary<home, Vector3>();
		populateOffsets();
	}
	void Update(){}
	
	public void SpawnHome(GameObject GridSpace){
		// TODO 
		// animation
		if(isVacant(GridSpace)){
			Vector3 offset = new Vector3(0, .5f, 0) + getCurrentOffset();
			Vector3 pos = GridSpace.transform.position + offset;
			GameObject toAdd = Instantiate(
				getCurrentHome(), pos, getCurrentHome().transform.rotation);
			toAdd.transform.SetParent(GridSpace.transform);
			if(isVacant(GridSpace)){
				int i = getIndex(GridSpace);
				homes[i] = toAdd;
				GetComponent<EconomyManager>().EnqueueCash(i.ToString(),new Vector2(50, 10));
				GetComponent<EconomyManager>().EnqueueCash(i.ToString(),new Vector2(-20, 15));
			}
		}
	}

	public GameObject getCurrentHome(){
		switch(currentHome){
			case home.A:
				return HomeA;
			case home.B:
				return HomeB;
			default:
				return HomeA;
		}
	}

	public bool DestroyHome(GameObject GridSpace){
		if(!isVacant(GridSpace)){
			int i = getIndex(GridSpace);
			Destroy(homes[i]);
			homes[i] = null;
			return true;
		}
		return false;
	}

	public void nextHome(){
		if((int)currentHome == (numHomeTypes - 1)){
			currentHome = home.A;
		}
		else{
			currentHome++;
		}
	}
	public void prevHome(){
		if((int)currentHome == 0){
			currentHome = (home)(numHomeTypes - 1);
		}
		else{
			currentHome--;
		}
	}

	private bool isVacant(GameObject GridSpace){
		int i = getIndex(GridSpace);
		if(homes[i] == null){ return true; }
		else { return false; }
	}
	
	private Vector3 getCurrentOffset(){
		return homeOffsets[currentHome];
	}
	private void populateOffsets(){
		homeOffsets[home.A] = new Vector3(0, .4f, 0);
		homeOffsets[home.B] = new Vector3(1.05f, .52f, .4f);
	}
	// Helper methods
	private int getIndex(GameObject GridSpace){
		return GetComponent<GridManager>().getIndex(GridSpace);
	}

	// Debug Gui
	void OnGUI(){
		// Home indicator
		if(GetComponent<InputManager>().getCurrentContext() == InputManager.context.build){
			string text = "";
			if(currentHome == home.A){ text = "Home: A"; }
			else if(currentHome == home.B){ text = "Home: B"; }
			GUI.Box(new Rect(0, 27, 100, 25), text);
		}
	}
}
