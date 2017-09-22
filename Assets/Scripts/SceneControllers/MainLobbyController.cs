using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : BaseSceneController
{
    [SerializeField] private Text tfName;
    [SerializeField] private Text tfLvl;
    [SerializeField] private Text tfMetal;
    [SerializeField] private Text tfMoney;


    void Awake()
    {
        Model.LoadUcars();
        Model.LoadMetal();
        Model.LoadMoney();
        Model.LoadChests();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToOfflineRacing()
    {
        ScreenManager.showScreen(ScreenManager.Screens.CHOOSE_CAR_MENU);
    }

    public void GoToOnlineRacing()
    {
        NotifyData.AddNew("Looking for updates");
    }
#if CHEATS
    public void AddChest()
    {
        UserChestData chest = new UserChestData();
        chest.Lvl = 1;
        chest.Rarity = ChestRarity.Common;
        chest.TimeWhenOpen = Utility.Time + 100;

        Model.UChests.Add(chest);
    }
#endif

    public void UpdateChests()
    {
        
    }
}
