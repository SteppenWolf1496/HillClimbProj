using System.Collections;
using System.Collections.Generic;
using GameUtility;
using UnityEngine;

public class TuningGuiController : MonoBehaviour {

    [SerializeField] protected TuningSlot[] slots = new TuningSlot[4];
	// Use this for initialization
	void Start () {
	    for (int i = 0; i < slots.Length; i++)
	    {
	        slots[i].num = i;
	    }
	    InitSlotsData();

	}


    private void InitSlotsData()
    {
        
        //Model.
        UserCarData uCardata = Model.GetUserCarData(CarChoosingController.currentCar.Key);
        if (uCardata.PassiveModifs.Count > 4)
        {
            Log.Error("Too Many Modifiers");
        }

        for (int i = 0; i < uCardata.PassiveModifs.Count; i++)
        {
            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
