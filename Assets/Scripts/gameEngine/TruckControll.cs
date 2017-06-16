using UnityEngine;
using System.Collections;
using System;
using Enums;


public class TruckControll : MonoBehaviour
{
    public Wheel[] wheels;

    Vector3 acceleration;
    Vector3 lastV;
    Vector3 currV;


    [Header("Suspension")]
    public float Spring = 90000;
    public float Damper = 9000;
    public float TargetPosition = 0.5f;


    [Header("Debug")]

    public float GForce;

    public bool accel = false;
    public bool breaking = false;
    public bool rear = false;

    public int MaxEngineRPM = 6000;



    [Header("Engine")]
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] public float engineMaxTorque = 300;
    [SerializeField] public float brakeForce = 25000;
    [SerializeField] public float handBrakeForce = 30000;
    
    private Vector3 startPosition;
    private Quaternion startRotaion;
    [SerializeField] public Transform CenterOfMass;

    private float[] torqByWheel;


    public float FlySpeedResuce = 0.05f;

    public float angolarCoef = 0.1f;

    public float[] gears = new float[5] {3.6f, 1.95f, 1.357f, 0.941f, 0.784f};
    public float rearGear = 3;
    public float maxSpeed = 180;
    private int curGear = 0; //between 0 - gearscount;
    private float engineTorque;

    public ParticleSystem[] exhaustSystem;
    private float speedbyGear;
    private float speed;

    [Header("Wheels")]
    public float ExtremumSlip = 0.4f;
    public float ExtremumValue = 1f;
    public float AsymptoteSlip = 0.8f;
    public float AsymptoteValue = 1f;
    public float Stiffness = 1f;

    int CompareCondition(Wheel itemA, Wheel itemB)
    {
        if (itemA.collider.radius*(itemA.isDrive ? 1 : 0) < itemB.collider.radius *(itemB.isDrive ? 1 : 0))
            return 1;
        if (itemA.collider.radius *(itemA.isDrive ? 1 : 0) > itemB.collider.radius *(itemB.isDrive ? 1 : 0))
            return -1;
        return 0;
    }

    void Awake()
    {
        if (!MainController.mainCamera)
        {
            MainController.mainCamera = Camera.main;
        }
    }

    private float radiusSumm;
    private int notDrive;
    private float middleTorq;

    void Start()
    {
       // rigid = this.GetComponent<Rigidbody>();

        rigid.centerOfMass = CenterOfMass.localPosition;

        startPosition = this.transform.position;
        startRotaion = this.transform.rotation;

        speedbyGear = maxSpeed/gears.Length;


        if (wheels.Length <= 0) return;
        if (gears.Length <= 0) return;
        /*int i = 0;
        while (i < wheels.Length)
        {
            if (wheels[i].isDrive)
            {
                tmpWheel = wheels[i].collider;
                break;
            }
            ++i;
        }*/

        Array.Sort(wheels, CompareCondition);

        radiusSumm = 0;
        //Array.Sort(wheels, CompareCondition);
        torqByWheel = new float[wheels.Length];
        notDrive = 0;
        WheelFrictionCurve curve;
        JointSpring joint;
        //JointSpring joint;
        foreach (var t in wheels)
        {

            // Set Suspension parameters ------------------------------------------
            joint = t.collider.suspensionSpring;
            joint.spring = Spring;
            joint.damper = Damper;
            joint.targetPosition = TargetPosition;

            t.collider.suspensionSpring = joint;




            // Set Wheels parameters ------------------------------------------
            curve = t.collider.forwardFriction;
            curve.extremumSlip = ExtremumSlip;
            curve.extremumValue = ExtremumValue;
            curve.asymptoteSlip = AsymptoteSlip;
            curve.asymptoteValue = AsymptoteValue;
            curve.stiffness = Stiffness;

            t.collider.forwardFriction = curve;

            if (t.isDrive)
                radiusSumm += t.collider.radius;
            else
                notDrive++;
        }

        engineTorque = gears[curGear] * engineMaxTorque;
        middleTorq = engineTorque / radiusSumm;

        inited = true;
    }

  /*  public float getSpeed()
    {
        return speed;
    }*/

    private float wheelMiddleRPM = 0;
    private float wheelsRPMSumm = 0;
    private void countTorque()
    {

        wheelsRPMSumm = 0;
        for (int i = 0; i < wheels.Length - notDrive; i++)
        {
            if (!rear && accel)
            {
                wheels[wheels.Length - i - 1 - notDrive].collider.motorTorque = ((middleTorq*1) /*/ wheels [i].radius*/);
            }
            else if (rear)
            {
                wheels[wheels.Length - i - 1 - notDrive].collider.motorTorque = -1*middleTorq;
            }
            else
            {
                wheels[wheels.Length - i - 1 - notDrive].collider.motorTorque = 0;
            }
            wheelsRPMSumm += wheels[wheels.Length - i - 1 - notDrive].collider.rpm;
            //	wheels [wheels.Length - i - 1 - notDrive].setWheelDefFreak (ExtremumSlip, ExtremumValue, AsymptoteSlip, AsymptoteValue);
        }
        wheelMiddleRPM = wheelsRPMSumm / wheels.Length - notDrive;

        for (int i = 0; i < wheels.Length; i++)
        {
            if ((breaking && !rear) || (accel && rigid.velocity.x < -1))
            {
                wheels[i].collider.brakeTorque = brakeForce;
            }
            else
            {
                wheels[i].collider.brakeTorque = 0;
            }
        }
    }

    private void updateGearValues()
    {
        //int gear;
        if (rear)
        {
            curGear = 0;
            engineTorque = rearGear*engineMaxTorque;
            countTorque();
            return;
        }
        //Debug.Log("COUNT GEAR");
        if (EngineRPM() >= (MaxEngineRPM - 2000))
        {
            UpGear();
        } else if (EngineRPM() < 1500)
        {
            DownGear();
        }

       
        
        countTorque();
    }

    private void UpGear()
    {
       // Debug.Log("TRY UP GEAR");
        if (curGear < gears.Length-1)
        {
            ++curGear;
            engineTorque = gears[curGear] * engineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
           // Debug.Log("UP GEAR");
        }
    }

    private void DownGear()
    {
        if (curGear > 0)
        {
            --curGear;
            engineTorque = gears[curGear] * engineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
            //Debug.Log("DOWN GEAR");
        }
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


    private bool inited = false;

    public bool Inited
    {
        get { return inited; }
    }
   
    public bool isGrounded()
    {
        foreach (Wheel w in wheels)
        {
            if (w.collider.isGrounded) return true;
               
        }
        return false;
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


        updateGearValues();
     
        if (!isGrounded() && !isDemo)
        {
            if (breaking)
            {
                rigid.angularVelocity = new Vector3(0, 0, rigid.angularVelocity.z - angolarCoef);
            }
            else if (accel)
            {
                rigid.angularVelocity = new Vector3(0, 0, rigid.angularVelocity.z + angolarCoef);
            }
            else
            {
            }
        }
        updateExhaustSystem();
    }

    void FixedUpdate()
    {
        lastV = currV;
        currV = rigid.velocity;
        acceleration = (currV - lastV)/Time.deltaTime;
        GForce = acceleration.magnitude/9.806f;

       /* Vector3 oldRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, oldRot.z);*/
    }

    void Update()
    {
       // speed = rigid.velocity.x*3.6f;
        if (!isGrounded())
        {
            if (rigid.velocity.x > 0)
            {
                Vector3 tmpVel = rigid.velocity;
                tmpVel.x -= FlySpeedResuce;
                rigid.velocity = tmpVel;
            }
            //rigidbody.velocity 
            rigid.drag = 0;
        }
        else
        {
            rigid.drag = drag;
        }


        CarMove();

        if (isDemo)
            MainController.mainCamera.transform.position = new Vector3(transform.position.x + 1,
                transform.position.y+7, -8);
        else
            MainController.mainCamera.transform.position = new Vector3(transform.position.x,
                transform.position.y+3+ Mathf.Abs(rigid.velocity.x/2), -8 - Mathf.Abs(rigid.velocity.x));

        
    }

   /* public WheelCollider tmpWheel = null;
    public float WheelRPM
    {

        get { if (tmpWheel == null) return 0;
            return tmpWheel.rpm;
        }
        
}*/

    private float tmpRPM = 0;
    private float drag = 0.2f;

    public float EngineRPM()
    {
        tmpRPM = wheelMiddleRPM * gears[curGear];
        return tmpRPM>MaxEngineRPM? MaxEngineRPM : tmpRPM ;
    }

    private void updateExhaustSystem()
    {
        if (exhaustSystem.Length == 0)
            return;

        float percent = EngineRPM() / MaxEngineRPM;
        float color = (1 - percent);
        ParticleSystem.MainModule main;
        ParticleSystem.EmissionModule emissionModule;

        float rateOverTime = 50 * percent;
        Color colorC = new Color(color, color, color);
        float startSpeed = 1f + 0.7f * (percent);
        float startSize = 0.3f + (percent);

        for (int i = 0; i < exhaustSystem.Length; i++)
        {
            main = exhaustSystem[i].main;
            emissionModule = exhaustSystem[i].emission;

            emissionModule.rateOverTime = rateOverTime;
            main.startColor = colorC;
            main.startSpeed = startSpeed;
            main.startSize = startSize;
        }
    }
}