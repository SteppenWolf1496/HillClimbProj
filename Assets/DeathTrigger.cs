using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathTrigger : MonoBehaviour
{

    protected TruckControll myCar;

    protected Collider collider;
	// Use this for initialization
	void Start ()
	{
	    myCar = GetComponentInParent<TruckControll>();
	    collider = GetComponent<BoxCollider>();
	    

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    void OnTriggerEnter(Collider item)
    {
        //item.
        myCar.BreakCar();
        //Debug.Log("Hello", gameObject);
        //Debug.Log("Hello");
        /* //Assume only one terrain displayer at a time
         var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;        
         if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
         {
             terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
         }*/


    }
}
