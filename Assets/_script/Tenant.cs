using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenant : MonoBehaviour {
	// Attaches to home prefab and holds tenant information
	private bool _isOccupied;
	private string _tenantName;
	private int _cash;
	private int _happiness;

	// Use this for initialization
	void Start () {
		_isOccupied = false;
	}

	public bool RegisterTenant(string name, int cash){
		if(!_isOccupied){
			_isOccupied = true;
			_tenantName = name;
			_cash = cash;
			_happiness = 0;
			return true;
		}
		return false;
	}

	public bool RemoveTenant(){
		if(_isOccupied){
			_tenantName = "None";
			_cash = 0;
			_isOccupied = false;
			return true;
		}
		return false;
	}

	public bool isOccupied(){
		return _isOccupied;
	}

	public string TenantName(){
		return _tenantName;
	}

	public int cash(){
		return _cash;
	}

	public int happiness(){
		return _happiness;
	}



}
