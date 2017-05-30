using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager : MonoBehaviour {
	public static PersistanceManager p;

	void Awake(){
		if(p == null){
			DontDestroyOnLoad(gameObject);
			p = this;
		}
		else if(p != this){
			Destroy(gameObject);
		}
	}
}
