using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour {
	public int startingCash = 400;
	public float updateInterval = 5;
	private int currentCash;
	private int cashUpdateAmount;
	private float timeOffset;

	// Use this for initialization
	void Start () {
		if(GetComponent<Persistance>().persistanceManager.GetComponent<PersistanceManager>().isSaved()){
			loadCash();
		}
		else{
			currentCash = startingCash;
			cashUpdateAmount = 0;
		}
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
		currentCash += cashUpdateAmount;
	}

	public void EnqueueCash(int cash){
	
		cashUpdateAmount += cash;
	}

	public void DequeueCash(int cash){
		cashUpdateAmount -= cash;
	}

	public int getCash(){
		return currentCash;
	}

	public void saveCash(){
		GetComponent<Persistance>().persistanceManager.
			GetComponent<PersistanceManager>().saveCash(currentCash, cashUpdateAmount);
	}

	private void loadCash(){
		int tempCash;
		cashUpdateAmount = GetComponent<Persistance>().persistanceManager.
			GetComponent<PersistanceManager>().loadCash(out tempCash);
		currentCash = tempCash;
	}

	void OnGUI(){
		if(GUI.Button(new Rect(250 ,2, 100, 100), "Get Cash Values")){
			Debug.Log("Current Cash:" + currentCash);
			Debug.Log("cashUpdateAmount: " + cashUpdateAmount);
		}

	}
}
