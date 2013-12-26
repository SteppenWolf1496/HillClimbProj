using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour
{
		//public LayerMask layer;
		//private Vector3 wheelStartTransform; //4
		public Transform wheelTransformBack = null; //4
		
		private WheelCollider[] colliders = new WheelCollider[5]; //5
		public WheelCollider defWheelCol;
		private Vector3 wheelStartPos; //6 
		private float rotation = 0.0f;  //7
		private float[] rotations = new float[]{-90.0f,-45.0f,0.0f,45.0f,90.0f};
		public bool isDrive = false;
		//public bool rotateWheelColliders = false;
		//private WheelCollider curCollider;
		private float wheelRadius, wheelOffset;
		private Quaternion defRotForDefCol;
		private float torq = 0;
		private bool inited = false;
		// Use this for initializationx
		void Start ()
		{
				inited = true;
				//updateBestCollider ();
				wheelStartPos = transform.localPosition;
				wheelRadius = defWheelCol.radius;
				wheelOffset = defWheelCol.suspensionDistance;
				defRotForDefCol = defWheelCol.transform.localRotation;
				//wheelStartTransform = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				//transform.position = defWheelCol.transform.position;
				// fill colliders

				for (int i=0; i<5; i++) {
						colliders [i] = generateWheelCollider (i, rotations [i]);
				}
		}

		public void setTorq (float torque)
		{
				torq = torque;
		}
	
		private WheelCollider generateWheelCollider (int i, float angle)
		{

				GameObject colliderObject = new GameObject ("WheelCollider" + i);
				colliderObject.transform.parent = defWheelCol.transform.parent;
				colliderObject.transform.position = defWheelCol.transform.position;
	
				WheelCollider retCollider = (WheelCollider)colliderObject.AddComponent (typeof(WheelCollider));
				//colliderObject.AddComponent(typeof());
				//retCollider.transform.rotation = defWheelCol.transform.rotation;
				//retCollider.transform.rotation = new Quaternion (0,3,1,1);
				colliderObject.transform.rotation = Quaternion.Euler (angle, 270, 0);

				//angle;
				retCollider.radius = defWheelCol.radius;
				retCollider.suspensionDistance = defWheelCol.suspensionDistance;
				retCollider.mass = 0;

				JointSpring t = new JointSpring ();
				t.spring = 0;
				t.damper = 0;
				retCollider.suspensionSpring = t;
				retCollider.forwardFriction = new WheelFrictionCurve ();
				retCollider.sidewaysFriction = new WheelFrictionCurve ();
				retCollider.radius = wheelRadius;

				colliderObject.layer = this.gameObject.layer;
				//retCollider.rigidbody = defWheelCol.rigidbody;

				//SetActiveRecursively (true);
				//retCollider.collider.
				return retCollider;
		}

		void FixedUpdate ()
		{
				updateBestCollider ();
		}
	
		void Update ()// Update is called once per frame
		{
				//updateBestCollider ();
		}
		//bool wasStopped = true;
		public void setTorque (float _accel, float _breake, float _carVelocity, float _breaktorque)
		{
				
				if (_breake != 0) {
						if (defWheelCol.attachedRigidbody.velocity.x > 1) {
								defWheelCol.motorTorque = 0;
								defWheelCol.brakeTorque = _breake * _breaktorque;
						} else {
								defWheelCol.brakeTorque = 0;
								if (isDrive)
										defWheelCol.motorTorque = _accel * torq;
						}
						//_accel = 0f;
				} else if (_accel != 0) {
						
						defWheelCol.brakeTorque = 0;
						if (isDrive)
								defWheelCol.motorTorque = _accel * torq;
				} else {
						defWheelCol.brakeTorque = 0;
						defWheelCol.motorTorque = 0;
				}
		}
	
		public void UpdateWheel (float _delta, Transform _transform)
		{
				if (inited == false)
						return;
				WheelHit hit;
				//defWheelCol.transform.localRotation = defRotForDefCol;
				Vector3 lp = transform.localPosition; //15
				
				if (defWheelCol.GetGroundHit (out hit)) { //16
						if (lp.y - (Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius) >= wheelStartPos.y/* || (Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius) < 0*/) {
								lp.y = wheelStartPos.y;
						} else {
								lp.y -= Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius; 
						}
				} else { 
			
						lp.y = wheelStartPos.y - wheelOffset; //18
						lp.z = wheelStartPos.z; //18
				}
				transform.localPosition = lp; //1
				if (wheelTransformBack != null) {
						wheelTransformBack.localPosition = lp; //19
				}
				
				rotation = Mathf.Repeat (rotation + _delta * defWheelCol.rpm * 360.0f / 60.0f, 360.0f); //20
				transform.localRotation = Quaternion.Euler (-rotation, 0f, 90.0f); //21
				
				
				//if (rotateWheelColliders)
				//	defWheelCol.transform.localRotation = Quaternion.Euler (Vector3.Dot (transform.position - hit.point, _transform.up), 180, 0f);//rigidbody.transform.rotation.x;

		}

		private void updateBestCollider ()
		{

				WheelCollider bestWheel = null;
		
				float bestHit = 0;
				WheelHit hit;
				//defWheelCol.transform.localRotation = defRotForDefCol;

				/*if (defWheelCol.GetGroundHit (out hit)) {
						bestWheel = defWheelCol;
						bestHit = hit.force;
					
				}*/
				foreach (WheelCollider col in colliders) {
						if (col.isGrounded) {
								if (bestWheel == null) {
										if (col.GetGroundHit (out hit)) {
												bestWheel = col;
												bestHit = hit.force;
					
										}
								}	
								if (col.GetGroundHit (out hit)) {
					
										if (bestHit < hit.force) {
												bestWheel = col;
												bestHit = hit.force;
										}
								}
						}
				}

				//Quaternion.Lerp
				if (bestWheel != null) {
						defWheelCol.transform.localRotation = Quaternion.Lerp (defWheelCol.transform.localRotation, bestWheel.transform.localRotation, Time.deltaTime * 0.03f);//bestWheel.transform.localRotation ;	
				} 

		}
}
