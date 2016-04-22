
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
        checkCar();
        (ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void PrevCar()
    {
        Model.curCarIndex--;
        checkCar();
        (ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void StartGame()
    {
        ScreenManager.showScreen(ScreenManager.Screens.GAME);
    }
	
		
		void checkCar ()
		{
				if (Model.curCarIndex < 0)
						Model.curCarIndex = Model.cars.Length - 1;

				if (Model.curCarIndex >= Model.cars.Length)
						Model.curCarIndex = 0;
		}
}




