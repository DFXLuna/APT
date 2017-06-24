using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager : MonoBehaviour {
	public static PersistanceManager p;
	public GameObject[] homes;

	void Awake(){
		if(p == null){
			DontDestroyOnLoad(gameObject);
			p = this;
		}
		else if(p != this){
			Destroy(gameObject);
		}
	}

	public void saveHouses(){
		homes = new GameObject[6];
		int i = 0;
		GameObject toSave = GameObject.FindWithTag("World");
		foreach(var h in toSave.GetComponent<HomeManager>().homes){
			homes[i] = h;
		}
	}
}
