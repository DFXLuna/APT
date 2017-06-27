using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NarrativeTypes;
using NarrativeReader;


public class NarrativeManager : MonoBehaviour {
	// Maps condition to associated narrative 
	private Dictionary<condition, narrative> narratives;
	private Reader reader;

	void Start () {
		narratives = new Dictionary<condition, narrative>();
		reader = new Reader("Assets/_script/test.slum", this);
		KeyValuePair<condition, narrative> test = reader.readNextScene();
	}
	
	void Update () {
		// This probably doesn't need to be called every frame
		List<condition> keys = new List<condition>(narratives.Keys);
		foreach(var k in keys){
			if(k()){
				callNarrative(narratives[k]);
				narratives.Remove(k);
			}
		}
		
	}

	public void registerNarrativeEvent(condition c, narrative n){
		narratives[c] = n;
	}

	private void callNarrative(narrative n){
		// Set up persistance here
		n();
	}

	// All the variables the reader would check are ints so this should be fine
	// Could change to a template if needed later
	public int getVariable(string name){
		if(string.Compare(name, "cash") == 0){
			return GetComponent<EconomyManager>().getCash();
		}
		else{
			Tenant t;
			if(GetComponent<TenantManager>().tryGetTenant(name, out t)){
				return t.happiness();
			}
		}
		return 0;
	}
}
