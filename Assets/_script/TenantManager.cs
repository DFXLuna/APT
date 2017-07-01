using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenantManager : MonoBehaviour {
	// Tenants
	private Dictionary<string, Tenant> tenants;

	void Start () {
		tenants = new Dictionary<string, Tenant>();	
	}

	public bool tryGetTenant(string name, out Tenant t){
		if(tenants.TryGetValue(name, out t)){
			return true;
		}
		//throw new System.Exception("Tenant " + name + " does not exist");
		return false;
	}

	public bool registerTenant(string name, Tenant t){
		Tenant temp;
		if(tenants.TryGetValue(name, out temp)){
			return false;
		}
		tenants[name] = t;
		return true;
	}
	
}
