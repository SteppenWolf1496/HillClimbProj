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
        [SerializeField] Transform arrow;



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
            arrow.rotation = Quaternion.Euler(0,0,truck.EngineRPM()/(float)truck.MaxEngineRPM*(-180.0f));


        }

      
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

