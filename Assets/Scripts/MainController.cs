using UnityEngine;
using System.Collections;
using Endless2DTerrain;

public class MainController : MonoBehaviour
{

		// Use this for initialization
		public Camera mainCamera;
		private Vector3 cameraStartPos;
		static private MainController inst;
		public void resetCamera ()
		{
				mainCamera.transform.position = cameraStartPos;
		}

		static public MainController instance ()
		{
				return inst;
		}
		void Start ()
		{

				Random.seed = 1;
				inst = this;
				cameraStartPos = transform.position;
				ScreenManager.showScreen (ScreenManager.Screens.START_MENU);
	
		}


		
	
		// Update is called once per frame
		void Update ()
		{

		}
}
