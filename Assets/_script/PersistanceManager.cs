using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeType;

public class PersistanceManager : MonoBehaviour {
	public static PersistanceManager p;
	private Home[] _homes;
	public bool _isSaved;

	void Awake(){
		if(p == null){
			DontDestroyOnLoad(gameObject);
			p = this;
			_isSaved = false;
			Debug.Log("Setting isSaved");
		}
		else if(p != this){
			Destroy(gameObject);
		}
	}

	public void saveHomes(Home[] homes){
		foreach(var h in homes){
			if(h != null){
				h.Save();
			}
		}
		// Hold reference to give back after loading
		_homes = homes;
		_isSaved = true;
		Debug.Log("Saved");
	}

	public bool tryLoadHomes(out Home[] ret){
		ret = null;
		if(_isSaved){
			ret = _homes;
			_isSaved = false;
			return true;
			_homes = null;
		}
		return false;
	}

	public bool isSaved(){
		return _homes != null;
	}
}
