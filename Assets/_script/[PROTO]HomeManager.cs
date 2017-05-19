using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour {
	public GameObject GridSpace;
	public int numSpaces = 6;
	public int spacesPerRow = 3;
	private List<GameObject> homes;
	private int[] spaces;

	// Use this for initialization
	void Start () {
		spaces = new int[numSpaces];
	}
	
	// Update is called once per frame
	void Update () {
		// S temp spawn homeA
		if(Input.GetKeyDown(KeyCode.S)){
			int space = findOpenSpace();
			// Each spot is 8.5 x 6.5 : 8 x 6 + .5 x .5 buffer
			if(space != -1){
				Vector2 xy = new Vector2((space % spacesPerRow) * 8,
										  (space / spacesPerRow) * 6);
				Vector3 pos = new Vector3(xy.x, 0, xy.y);
				Instantiate(GridSpace, pos, Quaternion.identity);
			}
		}
		
	}

    private int findOpenSpace(){
        int i = 0;
		while(i < numSpaces){
			if(spaces[i] != 1){
				spaces[i] = 1;
				return i;
			} 
			i++;
		}
		return -1;
    }
}
