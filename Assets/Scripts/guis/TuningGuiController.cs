using System;
using System.Collections;
using System.Collections.Generic;
using GameUtility;
using UnityEngine;

public class TuningGuiController : MonoBehaviour {

    [SerializeField] protected TuningSlot[] slots = new TuningSlot[4];
    [SerializeField] protected CardChoosingWindow cardChooseWindow;

    void Awake()
    {
       /* for (int i = 0; i < slots.Length; i++)
        {
            slots[i].init(i,this);
        }*/
    }
	// Use this for initialization
	void Start () {

        //  InitSlotsData();
	    cardChooseWindow.gameObject.SetActive(false);
	   // InitSlotsData();

	}


    public void InitSlotsData()
    {
        //return;
        //Model.
        UserCarData uCardata = Model.GetUserCarData(CarChoosingController.currentCar.Key);
        if (uCardata != null)
        {
           
            for (int i = 0; i < slots.Length; i++)
            {
                if (uCardata.PassiveModifs[i] != String.Empty)
                {
                    
                    slots[i].SetData(uCardata.PassiveModifs[i],  i, this,TuningSlot.SlotStatus.OnCar);
                }
                else
                {
                    slots[i].SetData(null,i, this, TuningSlot.SlotStatus.Empty);
                }

            }
        }
        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
               slots[i].SetData(null,i, this,TuningSlot.SlotStatus.Unactive);
            }

        }


        /*for (int i = 0; i < uCardata.PassiveModifs.Count; i++)
        {
            
        }*/
    }

    private int cardNum;
    public void ShowModifsWindow(int _num)
    {
        cardNum = _num;
        cardChooseWindow.gameObject.SetActive(true);
        cardChooseWindow.UpdateSlots(this);
    }

    public void SaveSelected(int _num, string _key)
    {
        cardNum = _num;
        cardChooseWindow.gameObject.SetActive(false);
        UserCarData uCardata = Model.GetUserCarData(CarChoosingController.currentCar.Key);
        uCardata.PassiveModifs[_num] = _key;
        InitSlotsData();
        Model.SaveUCars();
        ///cardChooseWindow.UpdateSlots(this);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
