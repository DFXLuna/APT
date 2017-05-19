using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour {
	public GameObject HomeA;
	// Indices match Grid array in GridManager
	private GameObject[] homes;

	void Start(){
		int numSpaces = GetComponent<GridManager>().numSpaces;
		homes = new GameObject[numSpaces];
	}
	void Update(){}
	public void SpawnHome(GameObject GridSpace){
		// TODO 
		// animation
		// .5 for GridSpace height + .4 for home offset
		if(isVacant(GridSpace)){
			Vector3 pos = GridSpace.transform.position + new Vector3(0, 0.9f, 0);
			GameObject toAdd = Instantiate(HomeA, pos, HomeA.transform.rotation);
			toAdd.transform.SetParent(GridSpace.transform);
			if(isVacant(GridSpace)){
				int i = getIndex(GridSpace);
				homes[i] = toAdd;
			}
		}
	}

	bool isVacant(GameObject GridSpace){
		int i = getIndex(GridSpace);
		if(homes[i] == null){ return true; }
		else { return false; }
	}

	int getIndex(GameObject GridSpace){
		return GetComponent<GridManager>().getIndex(GridSpace);
	}
}
