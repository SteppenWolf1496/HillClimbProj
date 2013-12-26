using UnityEngine;
using System.Collections;

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


		
		private static IScreen screenInstance;
		private static Screens curScreen;

		public static IScreen getCurScreen ()
		{
				return screenInstance;
		}


		public static void showScreen (Screens _name)
		{
				if (curScreen == _name)
						return;

				curScreen = _name;
				if (screenInstance != null) {
						screenInstance.destroy ();
						screenInstance = null;
				}
				MainController.instance ().resetCamera ();
				switch (_name) {
				case Screens.START_MENU:
						screenInstance = new StartMenuScreen ();
						(screenInstance as StartMenuScreen).create ();
						break;
				case Screens.GAME:
						screenInstance = new GameScreen ();
						(screenInstance as GameScreen).create ();
						break;
				}
		}



		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}

