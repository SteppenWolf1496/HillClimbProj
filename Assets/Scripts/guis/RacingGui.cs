using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.guis
{
    public class RacingGui : MonoBehaviour {
        // Use this for initialization
        [HideInInspector]
        public TruckControll truck;

        [SerializeField] AdditionalMouseEvents breakButton;
        [SerializeField] AdditionalMouseEvents accelButton;




        private GUIStyle style;
        void Start()
        {
            style = new GUIStyle();
            style.fontSize = 30;

            accelButton.AddOnMouseDownListener(OnAccelStart);
            accelButton.AddOnMouseUpListener(OnAccelStop);

            breakButton.AddOnMouseDownListener(OnBreakStart);
            breakButton.AddOnMouseUpListener(OnBreakStop);
        }

        void OnDestroy()
        {
            accelButton.RemoveOnMouseDownListener(OnAccelStart);
            accelButton.RemoveOnMouseUpListener(OnAccelStop);

            breakButton.RemoveOnMouseDownListener(OnBreakStart);
            breakButton.RemoveOnMouseUpListener(OnBreakStop);
        }

        
        // Update is called once per frame
        void Update()
        {

            /*
        #if UNITY_EDITOR
                        if (truck == null)
                                return;
                        float accel = 0;
                        float breake = 0;

                        if (Input.GetKey (KeyCode.RightArrow)) {
                                accel = -1;
                                breake = 0;
                        }
                        if (Input.GetKey (KeyCode.LeftArrow)) {
                                breake = 1;
                                accel = 1;
                        }


                        truck.setAcceleration (accel);
                        truck.setBreake (breake);
        #endif*/
        }

      /*  void OnGUI()
        {
            if (truck == null)
                return;

            float accel = 0;
            float breake = 0;

            
            float bWidth = UnityEngine.Screen.width / 5;
            float bHeight = UnityEngine.Screen.height / 3;


            if (GUI.RepeatButton(new Rect(Screen.width - bWidth, Screen.height - bHeight, bWidth, bHeight), "accelerate"))
            {
                accel = -1;
                breake = 0;
            }

            if (GUI.RepeatButton(new Rect(0, Screen.height - bHeight, bWidth, bHeight), "breake"))
            {
                breake = 1;
                accel = 1;
            }


            if (truck == null)
                return;
            truck.setAcceleration(accel);
            truck.setBreake(breake);

           // GUI.TextArea(new Rect(10, 10, UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 30), "speed: " + (int)truck.getSpeed() + "km/h; gear: " + (int)truck.getGear() + "g; torque = " + truck.getTorque(), 200, style);
        }
        */
        public void OnReset()
        {
            truck.reset();
        }

        public void OnExit()
        {
            ScreenManager.showScreen(ScreenManager.Screens.START_MENU);
        }

        public void OnBreakStart(PointerEventData data)
        {
            truck.breaking = true;
        }

        public void OnBreakStop(PointerEventData data)
        {
            truck.breaking = false;
        }


        public void OnAccelStart(PointerEventData data)
        {
            truck.accel = true;
        }

        public void OnAccelStop(PointerEventData data)
        {
            truck.accel = false;
        }

    }
}

