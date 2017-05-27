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
	// Maps a home enum to a transform offset
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
		if(isVacantLot(GridSpace)){
			Vector3 offset = new Vector3(0, .5f, 0) + getCurrentOffset();
			Vector3 pos = GridSpace.transform.position + offset;
			GameObject toAdd = Instantiate(
				getCurrentHomeModel(), pos, getCurrentHomeModel().transform.rotation);
			toAdd.transform.SetParent(GridSpace.transform);
			if(isVacantLot(GridSpace)){
				int i = getIndex(GridSpace);
				homes[i] = toAdd;
			}
		}
	}

	public GameObject getCurrentHomeModel(){
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
		if(!isVacantLot(GridSpace)){
			int i = getIndex(GridSpace);
			DequeueCash(i);
			DequeueCash(i + numHomeTypes);
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

	public bool registerTenant(string tenantName, int cash, GameObject home){
		if(home.GetComponent<Tenant>().RegisterTenant(tenantName, cash)){
			GetComponent<EconomyManager>().EnqueueCash(tenantName, cash);
			return true;
		}
		return false;
	}

	private bool isVacantLot(GameObject GridSpace){
		int i = getIndex(GridSpace);
		if(homes[i] == null){ return true; }
		else { return false; }
	}

	private bool isVacantHome(GameObject home){
		return !(home.GetComponent<Tenant>().isOccupied());
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

	private void EnqueueCash(string tenantName, int cash){
		GetComponent<EconomyManager>().EnqueueCash(tenantName, cash);
	}

	private void DequeueCash(int ID){
		GetComponent<EconomyManager>().DequeueCash(ID.ToString());
	}

	// Debug Gui
	void OnGUI(){
		// Home indicator
		if(GetComponent<InputManager>().getCurrentContext() == InputManager.context.build){
			string text = "";
			if(currentHome == home.A){ text = "Home: A"; }
			else if(currentHome == home.B){ text = "Home: B"; }
			GUI.Box(new Rect(0, 54, 100, 25), text);
		}
	}
}
