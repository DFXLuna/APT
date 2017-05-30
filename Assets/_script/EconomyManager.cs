using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour {
	public int startingCash = 400;
	private int currentCash;
	// Maps Tenent ID to cash amount
	private Dictionary<string, int> cashUpdateQueue;
	private float timeOffset;

	// Use this for initialization
	void Start () {
		currentCash = startingCash;
		cashUpdateQueue = new Dictionary<string, int>();
		timeOffset = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		// Temporary 
		if(Time.time - timeOffset >= 5){
			timeOffset = Time.time;
			updateCash();
		}
	}

	private void updateCash(){
		foreach(string t in cashUpdateQueue.Keys){
			currentCash += cashUpdateQueue[t];
		}
	}

	public void EnqueueCash(string tenentName, int cash){
		cashUpdateQueue[tenentName] = cash;
	}

	public bool DequeueCash(string tenentName){
		return cashUpdateQueue.Remove(tenentName);
	}

	public int getCash(){
		return currentCash;
	}

	void OnGUI(){
		string text = currentCash.ToString();
		GUI.Box(new Rect(0, 27, 100, 25), text);

	}
}
