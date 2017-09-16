using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.guis;
using UnityEngine;

public class RaceController : BaseSceneController
{
    private TerrainDisplayer terrain;
    private static TruckControll truck;
    private Vector3 startPosition;

    public RacingGui truckGUI;
    // Use this for initialization
    void Start () {
	    Object pPrefab = Resources.Load("perfabs/cars/" + Model.BaseCars[CarChoosingController.index].key);
	    truck = (MonoBehaviour.Instantiate(pPrefab) as GameObject).GetComponent<TruckControll>();
	    truck.gameObject.SetActive(true);
        
        truckGUI.truck = truck;
    }

    public static TruckControll Truck
    {
        get { return truck; }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    truckGUI.Distance.text = (truck.transform.position.x - startPosition.x).ToString();

	}
}
