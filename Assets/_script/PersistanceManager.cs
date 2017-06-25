using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeType;

public class PersistanceManager : MonoBehaviour {
	public static PersistanceManager p;
	private List<Home> _homes;

	void Awake(){
		if(p == null){
			DontDestroyOnLoad(gameObject);
			p = this;
			_homes = new List<Home>();
		}
		else if(p != this){
			Destroy(gameObject);
		}
	}

	public void saveHomes(Home[] homes){
		foreach(var h in homes){
			h.Save();
		}
		Debug.Log("Saved");
	}

	public void loadHomes()
}
