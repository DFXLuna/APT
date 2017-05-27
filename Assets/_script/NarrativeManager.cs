using NarrativeDelegates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeManager : MonoBehaviour {
	// Maps condition to associated narrative 
	private Dictionary<condition, narrative> narratives;

	void Start () {
		narratives = new Dictionary<condition, narrative>();
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

	// Allow for setup prior to n()
	private void callNarrative(narrative n){
		n();
	}
}
