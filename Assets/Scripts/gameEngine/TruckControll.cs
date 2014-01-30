using UnityEngine;
using System.Collections;
using System;


public class TruckControll : MonoBehaviour
{

		//public WheelCollider[] Colliders;
	
		public Wheel[] wheels;

		private float drag;
		
		private float wheelOffset = 0.5f; //2
		private float wheelRadius = 1f; //2
	
		//public float maxSteer = 300;
		public float engineTorque = 300;
		public float breakeTorque = 400;

		public float ExtremumSlip = 1;
		public float ExtremumValue = 20000;

		public float AsymptoteSlip = 2;
		public float AsymptoteValue = 10000;

		private Vector3 startPosition;
		private Quaternion startRotaion;
		//public Camera camera;
		public Transform CenterOfMass;
		private float _accel = 0;
		private float _breake = 0;
		private int driveWheels = 0;
		private float[] torqByWheel;



		public float FlySpeedResuce = 0.05f;

		public float angolarCoef = 0.1f;
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
				
				countTorque ();
				
				drag = rigidbody.drag;
		
		}

		private void countTorque ()
		{
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
						wheels [wheels.Length - i - 1 - notDrive].setWheelDefFreak (ExtremumSlip, ExtremumValue, AsymptoteSlip, AsymptoteValue);
				}
		}
		private bool isDemo = false;
		public void makeDEMO ()
		{
				engineTorque = engineTorque / 2;
				countTorque ();
				isDemo = true;
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
				
		}

		public bool isGrounded ()
		{
				bool ret = false;
				foreach (Wheel w in wheels) { 
						if (w.isGrounded ())
								ret = true;
				}

				return ret;
		}
	
		private void UpdateWheels ()
		{ 
				float delta = Time.deltaTime; 
		
		
				foreach (Wheel w in wheels) { 
						w.UpdateWheel (delta, transform);
				}	
		}
		
		private void CarMove (float accel, float breake)
		{
				if (isDemo) {
						accel = -1;
				}
				foreach (Wheel w in wheels) { 
						w.setTorque (accel, breake, transform.rigidbody.velocity.x, breakeTorque);
				}		
				if (!isGrounded ()) {

						if (_breake != 0) {
								rigidbody.angularVelocity = new Vector3 (0, 0, rigidbody.angularVelocity.z - angolarCoef);
								//rigidbody.AddTorque (0, 0, -5 * rigidbody.mass, ForceMode.Force);
								//_accel = 0f;
						} else if (_accel != 0) {
				
								rigidbody.angularVelocity = new Vector3 (0, 0, rigidbody.angularVelocity.z + angolarCoef);
						} else {
								//rigidbody.AddTorque (0, 0, 0, ForceMode.Force);
						}
						
				}
				
		
		}
	
		void Update ()
		{    

				if (!isGrounded ()) {
						if (rigidbody.velocity.x > 0) {
								Vector3 tmpVel = rigidbody.velocity;
								tmpVel.x -= FlySpeedResuce;
								//tmpVel.y += rigidbody.mass
								rigidbody.velocity = tmpVel;
						}
						//rigidbody.velocity
						rigidbody.drag = 0;
						
				} else {
						rigidbody.drag = drag;
				}
				UpdateWheels (); 
				CarMove (_accel, _breake);
				if (isDemo) 
						MainController.instance ().mainCamera.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, -8);
				else
						MainController.instance ().mainCamera.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, -8 - (rigidbody.velocity.x > 0 ? rigidbody.velocity.x / 1.5f : 0));
		}
	
}