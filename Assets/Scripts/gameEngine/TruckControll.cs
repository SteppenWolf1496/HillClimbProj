using UnityEngine;
using System.Collections;
using System;
using Enums;


public class TruckControll : MonoBehaviour
{
    // public WheelCollider[] Colliders;
    //public TypeOfDrive typeOfDrive;
    public UniversalWheel[] wheels;

    Vector3 acceleration;
    Vector3 lastV;
    Vector3 currV;
    public float GForce;

    public bool accel = false;
    public bool breaking = false;
    public bool rear = false;
    // Wheel
    /* public UniversalWheel frontLeftWheel;
     public UniversalWheel frontRightWheel;

     public UniversalWheel rearLeftWheel;
     public UniversalWheel rearRightWheel;*/

    [SerializeField] protected Rigidbody rigid;
    //private float drag;

    //private float wheelOffset = 0.5f; //2
    //private float wheelRadius = 1f; //2

    //public float maxSteer = 300;
    [SerializeField] public float engineMaxTorque = 300;
    [SerializeField] public float brakeForce = 25000;
    [SerializeField] public float handBrakeForce = 30000;
    //public float breakeTorque = 400;

    //public float ExtremumSlip = 1;
    //public float ExtremumValue = 20000;
    //
    //public float AsymptoteSlip = 2;
    //public float AsymptoteValue = 10000;

    private Vector3 startPosition;
    private Quaternion startRotaion;
    //public Camera camera;
    [SerializeField] public Transform CenterOfMass;

    ///private float _accel = 0;
    //private float _breake = 0;
    //private int driveWheels = 0;
    private float[] torqByWheel;


    public float FlySpeedResuce = 0.05f;

    public float angolarCoef = 0.1f;

    public float[] gears = new float[5] {3.6f, 1.95f, 1.357f, 0.941f, 0.784f};
    public float rearGear = 3;
    public float maxSpeed = 180;
    private int curGear = 9999; //between 0 - gearscount;
    private float engineTorque;

    public ParticleSystem[] exhaustSystem;
    private float speedbyGear;
    private float speed;
    // Use this for initialization
    //public Camera playerCamera;

    int CompareCondition(UniversalWheel itemA, UniversalWheel itemB)
    {
        if (itemA.radius*(itemA.isDrive ? 1 : 0) < itemB.radius*(itemB.isDrive ? 1 : 0))
            return 1;
        if (itemA.radius*(itemA.isDrive ? 1 : 0) > itemB.radius*(itemB.isDrive ? 1 : 0))
            return -1;
        return 0;
    }

    void Start()
    {
        if (!rigid) rigid = this.GetComponent<Rigidbody>();
        /*if (playerCamera == null) {
                playerCamera = Camera.main;
        }*/


        //playerCamera.transparencySortMode = TransparencySortMode.Orthographic;


        rigid.centerOfMass = CenterOfMass.localPosition;

        startPosition = this.transform.position;
        startRotaion = this.transform.rotation;


        //	drag = GetComponent<Rigidbody>().drag;
        speedbyGear = maxSpeed/gears.Length;
        //	updateGearValues (0);
    }

    public float getSpeed()
    {
        return speed;
    }

    private void countTorque()
    {
        float radiusSumm = 0;
        Array.Sort(wheels, CompareCondition);
        torqByWheel = new float[wheels.Length];
        int notDrive = 0;

        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].isDrive)
                radiusSumm += wheels[i].radius;
            else
                notDrive++;
        }

        float middleTorq = engineTorque/radiusSumm;

        for (int i = 0; i < wheels.Length - notDrive; i++)
        {
            if (!rear && accel)
            {
                wheels[wheels.Length - i - 1 - notDrive].axisTorque = ((middleTorq*1) /*/ wheels [i].radius*/);
            }
            else if (rear)
            {
                wheels[wheels.Length - i - 1 - notDrive].axisTorque = -1*middleTorq;
            }
            else
            {
                wheels[wheels.Length - i - 1 - notDrive].axisTorque = 0;
            }

            //	wheels [wheels.Length - i - 1 - notDrive].setWheelDefFreak (ExtremumSlip, ExtremumValue, AsymptoteSlip, AsymptoteValue);
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            if ((breaking && !rear) || (accel && rigid.velocity.x <0))
            {
                wheels[i].axisBrake = brakeForce;
            }
            else
            {
                wheels[i].axisBrake = 0;
            }
        }
    }

    private void updateGearValues()
    {
        int gear;
        if (rear)
        {
            gear = -1;
           /* if (gear == curGear)
                return;*/
            curGear = gear;
            engineTorque = rearGear*engineMaxTorque;
            countTorque();
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

      /*  if (gear == curGear)
            return;*/
        curGear = gear;
        engineTorque = gears[curGear]*(isDemo ? engineMaxTorque/2 : engineMaxTorque);
        countTorque();
    }

    private bool isDemo = false;

    public void makeDEMO()
    {
        engineMaxTorque = engineMaxTorque/2;
        isDemo = true;
        updateGearValues();
    }

    public int getGear()
    {
        return curGear;
    }

    public float getTorque()
    {
        return engineTorque;
    }

    public void reset()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10,
            this.transform.position.z);
        this.transform.rotation = startRotaion;
    }

    public void setAcceleration(float acc)
    {
        //_accel = acc;
    }

    public void setBreake(float bre)
    {
        //_breake = bre;
    }

    private bool inited = false;


    public bool isGrounded()
    {
        bool ret = false;
        foreach (UniversalWheel w in wheels)
        {
            if (w.HasContact)
                ret = true;
        }

        return ret;
    }

    private void UpdateWheels()
    {
        /*	float delta = Time.deltaTime; 
		
		
				foreach (Wheel w in wheels) { 
						w.UpdateWheel (delta, transform);
				}	*/
    }

    private void CarMove()
    {
        if (isDemo)
        {
            accel = true;
        }


        if (rigid.velocity.x <= 0.1 && breaking)
        {
            rear = true;
        }
        else
        {
            rear = false;
        }


        //float realAccel = 0;
        /*if (breake != 0) {
						if (rigid.velocity.x > 1 || !isGrounded ()) {

						} else {
								realAccel = accel;
						}
						//_accel = 0f;
				} else if (accel != 0) {
			
						realAccel = accel;
				} */


        updateGearValues();

        /*foreach (Wheel w in wheels) { 
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
						
				}*/
        updateExhaustSystem();
    }

    void FixedUpdate()
    {
        lastV = currV;
        currV = rigid.velocity;
        acceleration = (currV - lastV)/Time.deltaTime;
        GForce = acceleration.magnitude/9.806f;
    }

    void Update()
    {
        speed = rigid.velocity.x*3.6f;
        if (!isGrounded())
        {
            if (rigid.velocity.x > 0)
            {
                Vector3 tmpVel = rigid.velocity;
                tmpVel.x -= FlySpeedResuce;
                //tmpVel.y += rigidbody.mass
                rigid.velocity = tmpVel;
            }
            //rigidbody.velocity
            rigid.drag = 0;
        }
        else
        {
            //	GetComponent<Rigidbody>().drag = drag;
        }

        UpdateWheels();
        CarMove();

        if (isDemo)
            MainController.instance().mainCamera.transform.position = new Vector3(transform.position.x + 1,
                transform.position.y, -8);
        else
            MainController.instance().mainCamera.transform.position = new Vector3(transform.position.x,
                transform.position.y, -8 - (rigid.velocity.x > 0 ? rigid.velocity.x/1.5f : 0));

        /*if (Input.GetKey (KeyCode.O)) {
    rb.velocity = Vector3.zero;
}
if (Input.GetKey (KeyCode.H)) {
    rb.AddForce (transform.right * -force);
}
if (Input.GetKey (KeyCode.K)) {
    rb.AddForce (transform.right * force);
}

if (Input.GetKey (KeyCode.U)) {
    rb.AddForce (transform.forward * force);
}
if (Input.GetKey (KeyCode.J)) {
    rb.AddForce (transform.forward * -force);
}
if (Input.GetKey (KeyCode.R)) {
    Application.LoadLevel (0);
}
*/
        /*if (Input.GetKey (KeyCode.Space)) {
			rearRightWheel.axisBrake = brakeForce;
			rearLeftWheel.axisBrake = brakeForce;
			frontLeftWheel.axisBrake = brakeForce;
			frontRightWheel.axisBrake = brakeForce;
		} else if (Input.GetKey (KeyCode.RightShift)) {
			rearRightWheel.axisBrake = handBrakeForce;
			rearLeftWheel.axisBrake = handBrakeForce;
			frontLeftWheel.axisBrake = 0;
			frontRightWheel.axisBrake = 0;
		} else {
			rearRightWheel.axisBrake = 0;
			rearLeftWheel.axisBrake = 0;
			frontLeftWheel.axisBrake = 0;
			frontRightWheel.axisBrake = 0;
		}

		frontLeftWheel.transform.localEulerAngles = new Vector3 (0, Input.GetAxis ("Horizontal") * steerAngle, 0);
		frontRightWheel.transform.localEulerAngles = new Vector3 (0, Input.GetAxis ("Horizontal") * steerAngle, 0);
        */
        /*
		float _brakingForce = -Mathf.Clamp (Input.GetAxis ("Vertical"), -1, 0) * brakeForce;
		rearRightWheel.axisBrake = _brakingForce;
		rearLeftWheel.axisBrake = _brakingForce;
		frontLeftWheel.axisBrake = _brakingForce;
		frontRightWheel.axisBrake = _brakingForce;
		*/

        /*if (typeOfDrive == TypeOfDrive.RearDrive)
        {
            rearRightWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
            rearLeftWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
        }
        else if (typeOfDrive == TypeOfDrive.ForwardDrive)
        {
            frontLeftWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
            frontRightWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
        }
        else
        {
            rearRightWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
            rearLeftWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
            frontLeftWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
            frontRightWheel.axisTorque = Input.GetAxis("Vertical") * maxTorque;
        }*/
    }

    private void updateExhaustSystem()
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