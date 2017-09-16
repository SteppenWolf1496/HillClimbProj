using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyController : BaseSceneController
{

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
}
