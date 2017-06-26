using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeType;

public class HomeManager : MonoBehaviour {
	// Indices match Grid array in GridManager
	public Home[] homes;
	// Maps names to tenant objects
	private Dictionary<string, Tenant> tenants;
	private HomeFactory factory;

	void Start(){
		// Home Array
		Debug.Log(GetComponent<Persistance>().PersistanceManager.
			GetComponent<PersistanceManager>().isSaved());
		int numSpaces = GetComponent<GridManager>().numSpaces;
		if(GetComponent<Persistance>().PersistanceManager.
			GetComponent<PersistanceManager>().isSaved()){
				Debug.Log("Loading Homes");
				loadHomes();
			}
		else{
			Debug.Log("Creating Homes");	
			homes = new Home[numSpaces];
		}
		factory = HomeFactory.instance();
	}
	void Update(){}
	
	public void SpawnHome(GameObject GridSpace){
		// TODO 
		// animation
		if(isVacantLot(GridSpace)){
			Home toAdd = factory.makeHome(GridSpace);
			int i = getIndex(GridSpace);
			homes[i] = toAdd;
		}
	}

	private GameObject getCurrentHomeModel(){
		return factory.getCurrentHomeModel();
	}

	private HomeEnum getCurrentHome(){
		return factory.getCurrentHome();
	}

	public void nextHome(){
		factory.nextHome();
	}
	public void prevHome(){
		factory.prevHome();
	}

	public bool registerTenant(string tenantName, int cash, Home home){
		// Register tenant with Home object, economy manger and tenantmanger
		if(home.registerTenant(tenantName, cash)){
			GetComponent<EconomyManager>().EnqueueCash(tenantName, cash);
			Tenant t;
			if(home.tryGetTenant(out t)){
				GetComponent<TenantManager>().registerTenant(tenantName, t);
				return true;
			}
		}
		return false;
	}

	private bool isVacantLot(GameObject GridSpace){
		int i = getIndex(GridSpace);
		if(homes[i] == null){ return true; }
		else { return false; }
	}

	private bool isVacantHome(Home home){
		Tenant t;
		if(home.tryGetTenant(out t)){
			return false;
		}
		return true;
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

	public void saveHomes(){
		GetComponent<Persistance>().PersistanceManager.
			GetComponent<PersistanceManager>().saveHomes(homes);
	}

	private void loadHomes(){
		Home[] h = null;
		if(GetComponent<Persistance>().PersistanceManager.
			GetComponent<PersistanceManager>().tryLoadHomes(out h)){
				homes = h;
			}
		else{
			throw new Exception("Unable to load homes");
		}
		// Set gridspace parents
		GameObject gs;
		foreach(Home home in homes){
			if(home != null){
				gs = GetComponent<GridManager>().getGridSpace(home.getGridSpace());
				home.setParentGridspace(gs);
			}
		}
	}

	
	public Home getHomeFromGameObject(GameObject obj){
		foreach(var h in homes){
			if(h.compareWithGameObject(obj)){
				return h;
			}
		}
		throw new Exception("Error in getHomeFromGameObject " + obj);
	}

	// Debug Gui
	void OnGUI(){
		// Home indicator
		if(GetComponent<InputManager>().getCurrentContext() == InputManager.context.build){
			string text = "";
			if(getCurrentHome() == HomeEnum.A){ text = "Home: A"; }
			else if(getCurrentHome() == HomeEnum.B){ text = "Home: B"; }
			GUI.Box(new Rect(0, 54, 100, 25), text);
		}
	}
}


// Deprecated but possibly needed in the future
// public bool DestroyHome(GameObject GridSpace){
// 	if(!isVacantLot(GridSpace)){
// 		int i = getIndex(GridSpace);
// 		DequeueCash(i);
// 		DequeueCash(i + numHomeTypes);
// 		Destroy(homes[i]);
// 		homes[i] = null;
// 		return true;
// 	}
// 	return false;
// }