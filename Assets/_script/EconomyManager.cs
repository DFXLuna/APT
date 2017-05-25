using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// Deal with IDs

public class EconomyManager : MonoBehaviour {
	public int startingCash = 400;
	private int currentCash;
	// Maps cash ID to cash amount, update interval and time offset
	private Dictionary<string, Vector3> cashUpdateQueue;

	// Use this for initialization
	void Start () {
		currentCash = startingCash;
		cashUpdateQueue = new Dictionary<string, Vector3>();
		
	}
	
	// Update is called once per frame
	void Update () {
		updateCash();
	}

	private void updateCash(){
		List<string> keys = new List<string>(cashUpdateQueue.Keys);
		Vector3 temp;
		float t = Time.time;
		foreach(var k in keys){
			temp = cashUpdateQueue[k];
			if(t - temp.z >= temp.y){
				currentCash += (int)temp.x;
				temp.z = t;
				cashUpdateQueue[k] = temp;
			}
		}
	}

	public void EnqueueCash(string ID, Vector2 cashTime){
		Vector3 v = new Vector3(cashTime.x, cashTime.y, Time.time);
		cashUpdateQueue[ID] = v;
	}

	public bool DequeueCash(string ID){
		return cashUpdateQueue.Remove(ID);
	}

	void OnGUI(){
		string text = currentCash.ToString();
		GUI.Box(new Rect(0, 54, 100, 25), text);

	}
}
