using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour {
	public PersistanceManager persistanceManager;
	// Proxy for PersitanceManager

	public void Start(){
		registerWithWorldManager();
	}

	public void registerWithWorldManager(){
		persistanceManager = PersistanceManager.p;
	}
}
