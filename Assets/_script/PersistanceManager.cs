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
	}
}
