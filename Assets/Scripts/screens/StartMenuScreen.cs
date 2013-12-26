using UnityEngine;
using System.Collections;

public class StartMenuScreen : IScreen
{
		private MainGUI gui = null;
		private	TerrainDisplayer terrain = null;
		private TruckControll truck = null;


		override public void destroy ()
		{
				
				MonoBehaviour.DestroyImmediate (gui.gameObject, true);
				MonoBehaviour.DestroyImmediate (terrain.gameObject, true);
				MonoBehaviour.DestroyImmediate (truck.gameObject, true);
				
				gui = null;
				terrain = null;
				truck = null;
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				
		}

		public void changeCar ()
		{
				if (truck) {
						truck.gameObject.SetActive (false);
						MonoBehaviour.Destroy (truck.gameObject);
						truck = null;
				}

				if (truck == null) {
						
						UnityEngine.Object pPrefab = Resources.Load ("perfabs/cars/" + Model.cars [Model.curCarIndex]);
						truck = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(TruckControll)) as TruckControll;
				
				}

		}

		override public void create ()
		{
				UnityEngine.Object pPrefab;
				if (terrain == null) {
						pPrefab = Resources.Load ("perfabs/terrains/terrain1");
						terrain = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(TerrainDisplayer)) as TerrainDisplayer;
				}
				if (gui == null) {
						pPrefab = Resources.Load ("perfabs/GUI/MainGUI");
						gui = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(MainGUI)) as MainGUI;
				}
				if (truck == null) {
						pPrefab = Resources.Load ("perfabs/cars/" + Model.cars [Model.curCarIndex]);
						truck = (MonoBehaviour.Instantiate (pPrefab) as GameObject).GetComponent (typeof(TruckControll)) as TruckControll;
				}
		
		}
}

