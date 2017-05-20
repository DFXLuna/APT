using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
	public static bool FindChildwithTag(this GameObject parent, string tag, out Transform child){
		foreach(Transform t in parent.transform){
			if(t.tag == tag){
				child = t;
				return true;
			}
		}
		child = null;
		return false;
	}

}
