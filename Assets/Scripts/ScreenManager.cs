using System;
using UnityEngine;
using System.Collections;

using SSC;

public class ScreenManager : MonoBehaviour
{

		public enum Screens
		{
				NONE,
				START_MENU,
				CHOOSE_CAR_MENU,
				CHOOSE_MAP_MENU,
				GAME
		}


		
		
		private static Screens curScreen;

		


		public static void showScreen (Screens _name)
		{
			if (curScreen == _name)
				return;

			curScreen = _name;
		    switch (curScreen)
		    {
		        case Screens.NONE:
		            break;
		        case Screens.START_MENU:
                    SceneChangeManager.Instance.loadNextScene("MainLobby");
		            break;
		        case Screens.CHOOSE_CAR_MENU:
		            SceneChangeManager.Instance.loadNextScene("CarChoosing");
                break;
		        case Screens.CHOOSE_MAP_MENU:
                    SceneChangeManager.Instance.loadNextScene("MapChoosing");
                break;
		        case Screens.GAME:
		            LoadGame();

                   
                    break;
		        default:
		            throw new ArgumentOutOfRangeException();
		    }
		}

    private static void LoadGame()
    {
        switch (Model.curMap)
        {
            case "vilage":
                SceneChangeManager.Instance.loadNextScene("RaceWillage");
                break;
            case "city":
                SceneChangeManager.Instance.loadNextScene("City");
                break;


        }
    }




}

