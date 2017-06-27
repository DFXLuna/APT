using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeType;

public class PersistanceManager : MonoBehaviour {
	public static PersistanceManager p;
	private static Home[] _homes;
	private static bool _isSaved;
	private static int _cash;


	void Awake(){
		if(p == null){
			DontDestroyOnLoad(gameObject);
			p = this;
			_isSaved = false;
			_homes = new Home[6];
			_cash = 0;
		}
		else if(p != this){
			Destroy(gameObject);
		}
	}

	public void saveHomes(Home[] homes){
		for(int i = 0; i <homes.Length; i++){
			if(homes[i] != null){
				homes[i].Save();
			}
			_homes[i] = homes[i];
		}
		_isSaved = true;
	}

	public bool tryLoadHomes(out Home[] ret){
		ret = new Home[_homes.Length];
		if(_isSaved){
			for(int i = 0; i < _homes.Length; i++){
				if(_homes[i] != null){
					Debug.Log(i + " is not null");
					_homes[i].Load();
				}
				ret[i] = _homes[i];
			}
			_isSaved = false;
			return true;
		}
		return false;
	}
	
	public void saveCash(int cash){
		_cash = cash;
	}

	public int loadCash(){
		return _cash;
	}
	
	public bool isSaved(){
		return _isSaved;
	}
}
