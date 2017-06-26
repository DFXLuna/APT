using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeType;

public class HomeFactory{
    // Homes
    public GameObject HomeA;
	public GameObject HomeB;
	private Dictionary<HomeEnum, GameObject> HomeModels;

    private int numHomeTypes;
	private HomeEnum currentHome;
	// Maps a home enum to a transform offset
	private Dictionary<HomeEnum, Vector3> homeOffsets;
    private static HomeFactory _instance;
    private static object syncRoot = new object();

    private HomeFactory(){
		// Load home models
		HomeModels = new Dictionary<HomeEnum, GameObject>();
		LoadModels();
        // Home Selection
		currentHome = HomeEnum.A;
		numHomeTypes = Enum.GetValues(typeof(HomeEnum)).Length;
		// Offsets
		homeOffsets = new Dictionary<HomeEnum, Vector3>();
		populateOffsets();
    }

    // No clue if this needs to be thread safe
    public static HomeFactory instance(){
        if(_instance == null){
            lock(syncRoot){
                if(_instance == null){
                    _instance = new HomeFactory();
                }
            }
        }
        return _instance;
    }

    public Home makeHome(GameObject GridSpace){ 
        Vector3 offset = new Vector3(0, .5f, 0) + getCurrentOffset();
        Vector3 pos = GridSpace.transform.position + offset;
        GameObject ret = GameObject.Instantiate(
            getCurrentHomeModel(), pos, getCurrentHomeModel().transform.rotation);
        ret.transform.SetParent(GridSpace.transform);
		
		Home h = new Home(ret, GridSpace.name);
        return h;
    }

    // Helper Methods
    private void populateOffsets(){
		homeOffsets[HomeEnum.A] = new Vector3(0, .4f, 0);
		homeOffsets[HomeEnum.B] = new Vector3(1.05f, .52f, .4f);
	}

    private Vector3 getCurrentOffset(){
        return homeOffsets[currentHome];
    }
    
	public void nextHome(){
		if((int)currentHome == (numHomeTypes - 1)){
			currentHome = HomeEnum.A;
		}
		else{
			currentHome++;
		}
	}

	public void prevHome(){
		if((int)currentHome == 0){
			currentHome = (HomeEnum)(numHomeTypes - 1);
		}
		else{
			currentHome--;
		}
	}

    public GameObject getCurrentHomeModel(){
		return HomeModels[currentHome];
	}

	public void LoadModels(){
		var models = Enum.GetValues(typeof(HomeEnum));
		foreach(HomeEnum m in models){
			HomeModels[m] = Resources.Load<GameObject>("Home" + m.ToString()); 
		}
		foreach(HomeEnum m in models){
		}
		
	}

    public HomeEnum getCurrentHome(){
        return currentHome;
    }


}
