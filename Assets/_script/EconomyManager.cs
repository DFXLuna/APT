using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour {
	public int startingCash = 400;
	public float updateInterval = 5;
	private int currentCash;
	// Maps Tenent ID to cash amount
	private Dictionary<string, int> cashUpdateQueue;
	private float timeOffset;

	// Use this for initialization
	void Start () {
		if(GetComponent<Persistance>().persistanceManager.GetComponent<PersistanceManager>().isSaved()){
			loadCash();
		}
		else{
			currentCash = startingCash;
		}
		cashUpdateQueue = new Dictionary<string, int>();
		timeOffset = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		// Temporary 
		if(Time.time - timeOffset >= updateInterval){
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

	public void saveCash(){
		GetComponent<Persistance>().persistanceManager.GetComponent<PersistanceManager>().saveCash(currentCash);
	}

	private void loadCash(){
		currentCash = GetComponent<Persistance>().persistanceManager.GetComponent<PersistanceManager>().loadCash();
	}

	void OnGUI(){

	}
}
