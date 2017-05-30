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
		return false;
		//throw new System.Exception("Tenant " + name + " does not exist");
	}

	public bool registerTenant(string name, Tenant t){
		return false;
	}
	
}
