using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobbyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToRacing()
    {
        ScreenManager.showScreen(ScreenManager.Screens.CHOOSE_CAR_MENU);
    }
}
