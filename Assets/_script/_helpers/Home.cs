using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeType{
	// Enum describing types of homes
	public enum HomeEnum{ A, B };
	// Wrapper for gameobjects representing homes
	public class Home{
		GameObject _home;

		public Home(GameObject home){
			_home = home;
		}

		public bool registerTenant(string tenantName, int cash){
			// Register tenant with Home object, economy manger and tenantmanger
			if(_home.GetComponent<Tenant>().RegisterTenant(tenantName, cash)){
				return true;
			}
			return false;
		}

		public bool tryGetTenant(out Tenant t){
			t = null;
			if(_home.GetComponent<Tenant>().isOccupied()){
				t = _home.GetComponent<Tenant>();
				return true;
			}
			return false;
		}

		// public Home copy(){
		// 	GameObject g = GameObject.Instantiate(_home);
		// 	Home ret = new Home(g);
		// 	return ret;
		// }

		public void Save(){
			Object.DontDestroyOnLoad(_home);
			_home.SetActive(false);
		}

		//Used in collision detection for inputmanager
		public bool compareWithGameObject(GameObject check){
			return(_home == check);
		}
	}
}