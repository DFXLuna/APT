using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeType{
	// Enum describing types of homes
	public enum HomeEnum{ A, B };
	// Wrapper for gameobjects representing homes
	public class Home{
		private GameObject _home;
		private string _gridspace;

		public Home(GameObject home, string gridspace){
			_home = home;
			_gridspace = gridspace;
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

		public void Save(){
			// Have to remove parent before DontDestroyOnLoad
			_home.transform.parent = null;
			Object.DontDestroyOnLoad(_home);
			_home.SetActive(false);
		}

		public void Load(){
			_home.SetActive(true);
		}

		public string getGridSpace(){
			return _gridspace;
		}

		public void setParentGridspace(GameObject gridspace){
			_home.transform.parent = gridspace.transform;
		}

		//Used in collision detection for inputmanager
		public bool compareWithGameObject(GameObject check){
			return(_home == check);
		}
	}
}