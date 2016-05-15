using Assets.Scripts.guis;
using UnityEngine;

namespace Assets.Scripts.screens
{
    public class GameScreen : IScreen
    {
        private	TerrainDisplayer terrain;
        private TruckControll truck;
        private RacingGui truckGUI;

        override public void destroy ()
        {
            truckGUI.truck = null;
            MonoBehaviour.DestroyImmediate (terrain.gameObject, true);
            MonoBehaviour.DestroyImmediate (truck.gameObject, true);
            MonoBehaviour.DestroyImmediate (truckGUI.gameObject, true);
            terrain = null;
            truck = null;
            truckGUI = null;
        }
        // Use this for initialization
        void Start ()
        {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
	
        }

        override public void create ()
        {
		
            UnityEngine.Object pPrefab = Resources.Load ("perfabs/terrains/terrain1");
            terrain = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(TerrainDisplayer)) as TerrainDisplayer;
		
            pPrefab = Resources.Load ("perfabs/GUI/RacingGui");
            truckGUI = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(RacingGui)) as RacingGui;
            truckGUI.transform.SetParent(MainController.instance().GuiCanvas.transform,false);
            truckGUI.gameObject.SetActive(true);
           // truckGUI.transform.localScale = new Vector3(1f, 1f, 1f);

            pPrefab = Resources.Load ("perfabs/cars/" + Model.cars [Model.curCarIndex]);
            truck = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(TruckControll)) as TruckControll;
            truck.gameObject.SetActive(true);
            
            truckGUI.truck = truck;
		
        }
    }
}

