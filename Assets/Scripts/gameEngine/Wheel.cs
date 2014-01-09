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

		private bool isGroundedV = false;
		private float curSlip = 0;
		private float forceMagnitude = 0;

		public ParticleSystem particleSystem = null;
		// Use this for initializationx
		void Start ()
		{

				if (particleSystem)
						particleSystem.renderer.sortingLayerName = "Particles";
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
				updateParticles ();
		}
		//bool wasStopped = true;
		public void setTorque (float _accel, float _breake, float _carVelocity, float _breaktorque)
		{
				if (!this.gameObject.activeInHierarchy || !this.gameObject.activeSelf)
						return;
				if (_breake != 0) {
						if (defWheelCol.attachedRigidbody.velocity.x > 1 || !isGroundedV) {
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

		//private WheelFrictionCurve curveTemp;

		public void setWheelDefFreak (float _extremumSlip, float _extremumValue, float _asymptoteSlip, float _asymptoteValue)
		{
				WheelFrictionCurve curveTemp = defWheelCol.forwardFriction;
				curveTemp.extremumSlip = _extremumSlip;
				curveTemp.extremumValue = _extremumValue;
				curveTemp.asymptoteSlip = _asymptoteSlip;
				curveTemp.asymptoteValue = _asymptoteValue;
				defWheelCol.forwardFriction = curveTemp;
		}
	
		public void UpdateWheel (float _delta, Transform _transform)
		{
				if (inited == false)
						return;
				WheelHit hit;

				Vector3 lp = transform.localPosition; 
				//float slipV = 0;

				if (defWheelCol.GetGroundHit (out hit)) { 
						if (lp.y - (Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius) >= wheelStartPos.y/* || (Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius) < 0*/) {
								lp.y = wheelStartPos.y;
						} else {
								lp.y -= Vector3.Dot (transform.position - hit.point, _transform.up) - wheelRadius; 
						}
						//slipV = hit.forwardSlip;
				} else { 
			
						lp.y = wheelStartPos.y - wheelOffset; 
						lp.z = wheelStartPos.z; 
				}
				transform.localPosition = lp; 


				if (wheelTransformBack != null) {
						wheelTransformBack.localPosition = lp; 
				}
				float rotationslip = curSlip > 0 ? defWheelCol.rpm * curSlip : -defWheelCol.rpm * curSlip;
				rotation = Mathf.Repeat (rotation + _delta * (defWheelCol.rpm + rotationslip) * 360.0f / 60.0f, 360.0f); //20
				transform.localRotation = Quaternion.Euler (-rotation, 0f, 90.0f); //21
				
				
				//if (rotateWheelColliders)
				//	defWheelCol.transform.localRotation = Quaternion.Euler (Vector3.Dot (transform.position - hit.point, _transform.up), 180, 0f);//rigidbody.transform.rotation.x;

		}

		public bool isGrounded ()
		{
			
				return isGroundedV;
		}

		private void updateParticles ()
		{
				if (!particleSystem)
						return;

				/*	if (isDrive) {
						float rotationslip1 = curSlip > 0 ? defWheelCol.rpm * curSlip : -defWheelCol.rpm * curSlip;
						print ("RPM: " + (defWheelCol.rpm + rotationslip1));
				}*/
						
				/*if (!isDrive) {
						if (!particleSystem.isStopped)
								particleSystem.Stop ();
						return;
				} else */
				if (!isGroundedV) {
						if (!particleSystem.isStopped)
								particleSystem.Stop ();
						return;
				} /*else if (Mathf.Abs (curSlip) < Mathf.Abs (defWheelCol.forwardFriction.asymptoteSlip)) {
						if (!particleSystem.isStopped)
								particleSystem.Stop ();
						return;
				}*/ else if (particleSystem.isStopped) {
						particleSystem.Play ();

				}
				float rotationslip = curSlip > 0 ? defWheelCol.rpm * curSlip : -defWheelCol.rpm * curSlip;
				
				if (curSlip >= 0) {
						particleSystem.transform.localRotation = Quaternion.Euler (-45f, -180f, 0f);
						float coef = Mathf.Min (Mathf.Abs (curSlip), 2);
						particleSystem.emissionRate = 200 * (coef/* + forceMagnitude*/);
						particleSystem.startSpeed = 5 * coef;
				} else {
						particleSystem.transform.localRotation = Quaternion.Euler (-45f, 360f, 0f);
						float coef = Mathf.Min (Mathf.Abs (curSlip), 2);
						particleSystem.emissionRate = 200 * (coef/* + forceMagnitude*/);
						particleSystem.startSpeed = 5 * coef;
				}
		}

		private void updateBestCollider ()
		{
				isGroundedV = false;
				WheelCollider bestWheel = null;
				curSlip = 0;
				forceMagnitude = 0;
				float bestHit = 0;
				WheelHit hit;
				WheelFrictionCurve curve = defWheelCol.forwardFriction;
				foreach (WheelCollider col in colliders) {
						if (col.isGrounded) {
								if (bestWheel == null) {
										if (col.GetGroundHit (out hit)) {
												bestWheel = col;
												bestHit = hit.force;
												curve.stiffness = hit.collider.material.staticFriction;
										}
								}	
								if (col.GetGroundHit (out hit)) {
					
										if (bestHit < hit.force) {
												bestWheel = col;
												bestHit = hit.force;
												curve.stiffness = hit.collider.material.staticFriction;
										}
								}
						}
				}
				
				
				

				if (bestWheel != null) {
						isGroundedV = true;
						//curve.stiffness = curve.stiffness * forceMagnitude;


						defWheelCol.forwardFriction = curve;
						defWheelCol.transform.localRotation = Quaternion.Lerp (defWheelCol.transform.localRotation, bestWheel.transform.localRotation, 0.05f);//bestWheel.transform.localRotation ;	
				} 

				if (defWheelCol.GetGroundHit (out hit)) {
						curSlip = hit.forwardSlip;// / hit.collider.material.staticFriction;
						//curSlip = curSlip / 100;
						forceMagnitude = hit.force;
				}



		}
}
