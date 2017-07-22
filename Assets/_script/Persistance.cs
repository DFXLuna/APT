using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour {
	public PersistanceManager persistanceManager;
	// Proxy for PersitanceManager

	public void Start(){
		registerWithWorldManager();
	}

	private void registerWithWorldManager(){
		persistanceManager = PersistanceManager.p;
	}
}
