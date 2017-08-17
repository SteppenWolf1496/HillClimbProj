using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.guis;
using UnityEngine;

public class RaceController : MonoBehaviour {
    private TerrainDisplayer terrain;
    private TruckControll truck;
    public RacingGui truckGUI;
    // Use this for initialization
    void Start () {
	    Object pPrefab = Resources.Load("perfabs/cars/" + Model.BaseCars[CarChoosingController.index].key);
	    truck = (MonoBehaviour.Instantiate(pPrefab) as GameObject).GetComponent<TruckControll>();
	    truck.gameObject.SetActive(true);
        
        truckGUI.truck = truck;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
