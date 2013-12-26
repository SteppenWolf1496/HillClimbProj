using UnityEngine;
using System.Collections;
using System;


public class TruckControll : MonoBehaviour
{

		//public WheelCollider[] Colliders;
	
		public Wheel[] wheels;
		
		private float wheelOffset = 0.5f; //2
		private float wheelRadius = 1f; //2
	
		//public float maxSteer = 300;
		public float engineTorque = 300;
		public float breakeTorque = 400;
		private Vector3 startPosition;
		private Quaternion startRotaion;
		//public Camera camera;
		public Transform CenterOfMass;
		private float _accel = 0;
		private float _breake = 0;
		private int driveWheels = 0;
		private float[] torqByWheel;
			
		// Use this for initialization
		//public Camera playerCamera;
		
		int CompareCondition (Wheel itemA, Wheel itemB)
		{
				if (itemA.defWheelCol.radius * (itemA.isDrive ? 1 : 0) < itemB.defWheelCol.radius * (itemB.isDrive ? 1 : 0))
						return 1;
				if (itemA.defWheelCol.radius * (itemA.isDrive ? 1 : 0) > itemB.defWheelCol.radius * (itemB.isDrive ? 1 : 0))
						return -1;
				return 0;
		}

		void Start ()
		{

				/*if (playerCamera == null) {
						playerCamera = Camera.main;
				}*/
		
		
				//playerCamera.transparencySortMode = TransparencySortMode.Orthographic;

				rigidbody.centerOfMass = CenterOfMass.localPosition;

				startPosition = this.transform.position;
				startRotaion = this.transform.rotation;
				
				float radiusSumm = 0;
				Array.Sort (wheels, CompareCondition);
				torqByWheel = new float[wheels.Length];
				int notDrive = 0;

				for (int i=0; i<wheels.Length; i++) {
						if (wheels [i].isDrive)
								radiusSumm += wheels [i].defWheelCol.radius;
						else 
								notDrive++;
				}

				float middleTorq = engineTorque / radiusSumm;

				for (int i=0; i<wheels.Length-notDrive; i++) {
						wheels [wheels.Length - i - 1 - notDrive].setTorq (wheels [i].defWheelCol.radius * middleTorq);
				}
				
				
		
		}
	
		public void reset ()
		{
				this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
				this.transform.rotation = startRotaion;
		}
	
		public void setAcceleration (float acc)
		{
				_accel = acc;
		}
	
		public void setBreake (float bre)
		{
				_breake = bre;
		}
		private bool inited = false;
		void FixedUpdate ()
		{
				CarMove (_accel, _breake);
		

		
		}
	
		private void UpdateWheels ()
		{ 
				float delta = Time.fixedDeltaTime; 
		
		
				foreach (Wheel w in wheels) { 
						w.UpdateWheel (delta, transform);
				}	
		}
		
		private void CarMove (float accel, float breake)
		{
				
				foreach (Wheel w in wheels) { 
						w.setTorque (accel, breake, transform.rigidbody.velocity.x, breakeTorque);
				}		
		
				
		
		}
	
		void Update ()
		{    
				UpdateWheels (); 
		MainController.instance ().mainCamera.transform.position = new Vector3 ( transform.position.x+2, transform.position.y, -10 - (rigidbody.velocity.x > 0 ? rigidbody.velocity.x / 1.5f : 0));
		}
	
}