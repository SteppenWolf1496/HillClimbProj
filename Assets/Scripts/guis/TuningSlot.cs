using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TuningSlot : MonoBehaviour
{
    public enum SlotStatus
    {
        Empty,
        Unactive,
        OnCar,
        Selecting
    }
     

    [SerializeField] protected Text header;
    [SerializeField] protected Button change;
    [SerializeField] protected ProgressBar upgradeProgress;


    private UserCard uCardData;
    private Card cardData;
    private SlotStatus status;

    private TuningGuiController controller;
    private int num;

    private SlotStatus Status
    {
        get
        {
            return status;
        }

       
    }

    public void SetData(string _cardKey, int _num, TuningGuiController _controller, SlotStatus _status)
    {
        cardData = Model.GetCardData(_cardKey);
        uCardData = Model.GetUserCard(_cardKey);

        if (cardData != null)
        {
            
            header.text = cardData.Title;
            if (uCardData != null)
            {
                upgradeProgress.Value =100- uCardData.CollectedItems / cardData.NeedCollectForLvl * 100;
            }
            else
            {
                upgradeProgress.Value = 0;
            }
        }
        else
        {
            upgradeProgress.Value = 0;
            header.text = "Empty";
        }

        num = _num;
        controller = _controller;
        status = _status;
       
        if (status == SlotStatus.Unactive || (status == SlotStatus.Selecting && uCardData == null))
        {
            change.gameObject.SetActive(false);
        }
        else
        {
            change.gameObject.SetActive(true);
        }

        
    }

    public void ChooseModifier()
    {
        switch (Status)
        {
            case SlotStatus.Empty:
                controller.ShowModifsWindow(num);
                break;
            case SlotStatus.OnCar:
                controller.ShowModifsWindow(num);
                break;
            case SlotStatus.Selecting:
                controller.SaveSelected(num, cardData.key);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
      
    }
}
