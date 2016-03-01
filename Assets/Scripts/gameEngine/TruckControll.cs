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
		public float engineMaxTorque = 300;

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

		public float[] gears = new float[5]{3.6f, 1.95f, 1.357f, 0.941f, 0.784f};
		public float rearGear = 3;
		public float maxSpeed = 180;
		private int curGear = 9999;//between 0 - gearscount;
		private float engineTorque;

		public ParticleSystem[] exhaustSystem;
		private float speedbyGear;
		private float speed;
		// Use this for initialization
		//public Camera playerCamera;
		
		int CompareCondition (Wheel itemA, Wheel itemB)
		{
			/*	if (itemA.defWheelCol.radius * (itemA.isDrive ? 1 : 0) < itemB.defWheelCol.radius * (itemB.isDrive ? 1 : 0))
						return 1;
				if (itemA.defWheelCol.radius * (itemA.isDrive ? 1 : 0) > itemB.defWheelCol.radius * (itemB.isDrive ? 1 : 0))
						return -1;*/
				return 0;
		}

		void Start ()
		{

				/*if (playerCamera == null) {
						playerCamera = Camera.main;
				}*/
		
		
				//playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
/*

				GetComponent<Rigidbody>().centerOfMass = CenterOfMass.localPosition;

				startPosition = this.transform.position;
				startRotaion = this.transform.rotation;
				
				
				
				drag = GetComponent<Rigidbody>().drag;
				speedbyGear = maxSpeed / gears.Length;*/
				//	updateGearValues (0);
		
		}

		public float getSpeed ()
		{
				return speed;
		}

		private void countTorque ()
		{
				/*float radiusSumm = 0;
				Array.Sort (wheels, CompareCondition);
				torqByWheel = new float[wheels.Length];
				int notDrive = 0;
		
				/ *for (int i=0; i<wheels.Length; i++) {
						if (wheels [i].isDrive)
								radiusSumm += wheels [i].defWheelCol.radius;
						else 
								notDrive++;
				}* /
		
				float middleTorq = engineTorque / radiusSumm;*/
		
				/*for (int i=0; i<wheels.Length-notDrive; i++) {
						wheels [wheels.Length - i - 1 - notDrive].setTorq ((middleTorq * 1) / wheels [i].defWheelCol.radius);
						wheels [wheels.Length - i - 1 - notDrive].setWheelDefFreak (ExtremumSlip, ExtremumValue, AsymptoteSlip, AsymptoteValue);
				}*/
		}

		private void updateGearValues (float accel)
		{
				/*int gear;
				if (accel > 0) {
						gear = -1;
						if (gear == curGear)
								return;
						curGear = gear;
						engineTorque = rearGear * engineMaxTorque;
						countTorque ();
						return;

				}

		    if (speed > 0)
		    {
		        gear = (int) (speed/speedbyGear);
		    }
		    else
		    {
		        gear = 0;
		    }
		    if (gear >= gears.Length)
						gear = gears.Length - 1;

				if (gear == curGear)
						return;
				curGear = gear;
				engineTorque = gears [curGear] * (isDemo ? engineMaxTorque / 2 : engineMaxTorque);
				countTorque ();*/
		}
		private bool isDemo = false;

		public void makeDEMO ()
		{
				//engineMaxTorque = engineMaxTorque / 2;
				isDemo = true;
				//	updateGearValues (0);
				
		}

		public int getGear ()
		{
				return curGear;
		}

		public float getTorque ()
		{
				return engineTorque;
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
			/*	foreach (Wheel w in wheels) { 
						if (w.isGrounded ())
								ret = true;
				}*/

				return ret;
		}
	
		private void UpdateWheels ()
		{ 
				/*float delta = Time.deltaTime; 
		
		
				foreach (Wheel w in wheels) { 
						w.UpdateWheel (delta, transform);
				}	*/
		}
		
		private void CarMove (float accel, float breake)
		{
			/*	if (isDemo) {
						accel = -1;
				}

				float realAccel = 0;
				if (breake != 0) {
						if (transform.GetComponent<Rigidbody>().velocity.x > 1 || !isGrounded ()) {

						} else {
								realAccel = accel;
						}
						//_accel = 0f;
				} else if (accel != 0) {
			
						realAccel = accel;
				} 


				updateGearValues (realAccel);

				foreach (Wheel w in wheels) { 
						w.setTorque (accel, breake, transform.GetComponent<Rigidbody>().velocity.x, breakeTorque);
				}		
				if (!isGrounded ()) {

						if (_breake != 0) {
								GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, GetComponent<Rigidbody>().angularVelocity.z - angolarCoef);
								//rigidbody.AddTorque (0, 0, -5 * rigidbody.mass, ForceMode.Force);
								//_accel = 0f;
						} else if (_accel != 0) {
				
								GetComponent<Rigidbody>().angularVelocity = new Vector3 (0, 0, GetComponent<Rigidbody>().angularVelocity.z + angolarCoef);
						} else {
								//rigidbody.AddTorque (0, 0, 0, ForceMode.Force);
						}
						
				}
				updateExhaustSystem (realAccel);
				*/
		
		}
	
		void Update ()
		{
		   /* speed = this.GetComponent<Rigidbody>().velocity.x * 3.6f;
				if (!isGrounded ()) {
						if (GetComponent<Rigidbody>().velocity.x > 0) {
								Vector3 tmpVel = GetComponent<Rigidbody>().velocity;
								tmpVel.x -= FlySpeedResuce;
								//tmpVel.y += rigidbody.mass
								GetComponent<Rigidbody>().velocity = tmpVel;
						}
						//rigidbody.velocity
						GetComponent<Rigidbody>().drag = 0;
						
				} else {
						GetComponent<Rigidbody>().drag = drag;
				}
                
				UpdateWheels (); 
				CarMove (_accel, _breake);
                */
                if (isDemo) 
						MainController.instance ().mainCamera.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, -8);
				else
						MainController.instance ().mainCamera.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, -8 - (GetComponent<Rigidbody>().velocity.x > 0 ? GetComponent<Rigidbody>().velocity.x / 1.5f : 0));

				
		}

		private void updateExhaustSystem (float accel)
		{
				/*if (exhaustSystem.Length == 0)
						return;
				accel = Mathf.Abs (accel);
				float percent = Mathf.Abs (speed) % speedbyGear / speedbyGear;

				float coef = 0.1f + (accel == 0 ? 0 : 1);
				float color = 0.7f - percent * accel * 0.5f;

				for (int i =0; i<exhaustSystem.Length; i++) {
						exhaustSystem [i].emissionRate = 70 * (coef/ * + forceMagnitude* /);
						exhaustSystem [i].startColor = new Color (color, color, color);
						exhaustSystem [i].startSpeed = 1f + 15f * accel;
						exhaustSystem [i].startSize = 0.3f + 1f * accel;
				}*/
		}
	
}