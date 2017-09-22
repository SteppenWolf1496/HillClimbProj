using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChoosingWindow : MonoBehaviour
{
    private TuningSlot[] baseSlots;
    private List<TuningSlot> slots = new List<TuningSlot>();
    private TuningGuiController controller;
 

	// Use this for initialization
	void Awake ()
	{
	    baseSlots = GetComponentsInChildren<TuningSlot>();

	    foreach (TuningSlot slot in baseSlots)
	    {
	        slots.Add(slot);
	    }

        //UpdateSlots();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AddNeededSlots(int _num)
    {
        if (_num == 0) return;
        if (_num<0)
        {
            int count = Math.Abs(_num);
            for (int i = slots.Count - 1; i >= count; i--)
            {
                slots[i].gameObject.SetActive(false);
            }
        } else
        {
            slots.Add(Instantiate(baseSlots[0], baseSlots[0].transform.parent,false));
            //slots[slots.Count-1].transform.SetParent(baseSlots[0].transform.parent);
        }

    }

    public void UpdateSlots(TuningGuiController _controller)
    {
        //TODO: Need to remove already selected on car
        controller = _controller;
        
        AddNeededSlots(Model.Cards.Count - slots.Count);
        int i = 0;
        
        foreach (Card carModif in Model.Cards)
        {
            
            slots[i].SetData(carModif.key, i, controller,TuningSlot.SlotStatus.Selecting);
            ++i;
        }
        
    }
}
