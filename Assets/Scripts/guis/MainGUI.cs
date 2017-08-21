
using UnityEngine;
using System.Collections;
public class MainGUI : MonoBehaviour
{
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject buyBtn;


    //private GUIStyle style;
    void Start ()
		{
        //style = new GUIStyle ();
        //style.fontSize = 30;
		    UpdateButtons();
    }
	
		// Update is called once per frame
		void Update ()
		{
		
		}

    public void UpdateButtons()
    {
        bool have = Model.UserCars.Exists(x => x.key == CarChoosingController.Instance.CurrentCarKey);

        startBtn.SetActive(have);
        buyBtn.SetActive(!have);
    }

    public void NextCar()
    {
        Model.curCarIndex++;
       
        CarChoosingController.Instance.NextCar();
        UpdateButtons();

        // (ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void PrevCar()
    {
        Model.curCarIndex--;
        
        CarChoosingController.Instance.PrewCar();
        UpdateButtons();
        //(ScreenManager.getCurScreen() as StartMenuScreen).changeCar();
    }

    public void StartGame()
    {
        ScreenManager.showScreen(ScreenManager.Screens.CHOOSE_MAP_MENU);
    }

    public void MainScreen()
    {
        ScreenManager.showScreen(ScreenManager.Screens.START_MENU);
    }


   
}




