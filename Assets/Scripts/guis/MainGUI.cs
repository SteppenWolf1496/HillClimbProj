
using UnityEngine;
using System.Collections;
public class MainGUI : MonoBehaviour
{
		//private GUIStyle style;
		void Start ()
		{
				//style = new GUIStyle ();
				//style.fontSize = 30;
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

    public void NextCar()
    {
        Model.curCarIndex++;
       
        CarChoosingController.Instance.NextCar();
        // (ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void PrevCar()
    {
        Model.curCarIndex--;
        
        CarChoosingController.Instance.PrewCar();
        //(ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void StartGame()
    {
        ScreenManager.showScreen(ScreenManager.Screens.GAME);
    }

    public void MainScreen()
    {
        ScreenManager.showScreen(ScreenManager.Screens.START_MENU);
    }


   
}




