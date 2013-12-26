using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{

		// Use this for initialization
		public TruckControll truck;
		private GUIStyle style;
		void Start ()
		{
				style = new GUIStyle ();
				style.fontSize = 30;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
		void OnGUI ()
		{
				if (truck == null)
						return;
				float accel = 0;
				float breake = 0;
		
		 
		
				if (GUI.Button (new Rect (10, 30, 150, 100), "reset")) {
			
						truck.reset ();
				}
				if (GUI.Button (new Rect (Screen.width - 150, 0, 150, 100), "exit")) {
			
						ScreenManager.showScreen (ScreenManager.Screens.START_MENU);
				}


				float bWidth = UnityEngine.Screen.width / 5;
				float bHeight = UnityEngine.Screen.height / 3;
				if (GUI.RepeatButton (new Rect (Screen.width - bWidth, Screen.height - bHeight, bWidth, bHeight), "accelerate")) {
						accel = -1;
						breake = 0;
				}
				
				if (GUI.RepeatButton (new Rect (0, Screen.height - bHeight, bWidth, bHeight), "breake")) {
						breake = 1;
						accel = 1;
				}
				if (truck == null)
						return;
				truck.setAcceleration (accel);
				truck.setBreake (breake);
		
				GUI.TextArea (new Rect (10, 10, UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 30), "speed: " + (int)truck.rigidbody.velocity.magnitude * 3.6 + "km/h; distance: " + (int)truck.rigidbody.position.x + "m", 200, style);
		}
}
