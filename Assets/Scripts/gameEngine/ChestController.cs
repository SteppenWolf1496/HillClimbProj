using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Enums;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private float updateCounter;
    private const float UPDATE_TIME = 0.5f;
	
	// Update is called once per frame
	void Update ()
	{
	    updateCounter += Time.deltaTime;
	    if (updateCounter < UPDATE_TIME) return;

	    updateCounter = 0;

	    UpdateChests();

	}

    private void UpdateChests()
    {
        bool needSave = false;
        foreach (UserChestData chest in Model.UChests)
        {
            if (chest.state != ChestState.Opening) continue;
            if (chest.TimeWhenOpen <= Utility.TimeInt)
            {
                chest.TimeWhenOpen = 0;
                chest.state = ChestState.Opened;
                needSave = true;
            }
        }

        if (needSave)
        {
            
            Model.SaveChests();
            MainLobbyController.Instance.UpdateChests();
        }
    }

    public static bool CanStartOpening()
    {
        foreach (UserChestData userChestData in Model.UChests)
        {
            if (userChestData.state == ChestState.Opening)
            {
                return false;
            }
        }
        return true;
    }
}
