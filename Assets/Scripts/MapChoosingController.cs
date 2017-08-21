using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChoosingController : MonoBehaviour
{

    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject buyBtn;


    [SerializeField] protected TerrainDisplayer displayer;

    [SerializeField] protected GameObject mapsGO;

    protected MapChoosingData[] maps;

    private TruckControll truck;

    // Use this for initialization
    void Start () {

        Object pPrefab = Resources.Load("perfabs/cars/" + Model.BaseCars[CarChoosingController.index].key);
        truck = (MonoBehaviour.Instantiate(pPrefab) as GameObject).GetComponent<TruckControll>();
        truck.gameObject.SetActive(true);
        

        buyBtn.SetActive(false);

        maps = mapsGO.GetComponents<MapChoosingData>();

        SetMapData();

        StartCoroutine(StartCar());
    }

    private IEnumerator StartCar()
    {
        while (truck.inited == false)
        {
            yield return new WaitForSeconds(1);
           
        }
        truck.makeDEMO();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMapData()
    {
        displayer.DetailMaterial = maps[Model.curMapIndex].Detail;
        displayer.MainMaterial = maps[Model.curMapIndex].Front;
        displayer.TopMaterial = maps[Model.curMapIndex].Top;

        displayer.PrefabRules[0].PrefabToClone = maps[Model.curMapIndex].Prefub1;
        displayer.PrefabRules[1].PrefabToClone = maps[Model.curMapIndex].Prefub2;

        Model.curMap = maps[Model.curMapIndex].Key;

        displayer.Setup();
        /* if (gui)
         {
             
         }*/
    }

    public void NextMap()
    {
        Model.curMapIndex++;
        if (Model.curMapIndex >= maps.Length)
        {
            Model.curMapIndex = 0;
        }
        SetMapData();
    }

    public void PrewMap()
    {
        Model.curMapIndex--;
        if (Model.curMapIndex < 0)
        {
            Model.curMapIndex = maps.Length-1;
        }
        SetMapData();
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
