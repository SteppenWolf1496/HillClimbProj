using UnityEngine;
using System.Collections;
using System;
using Enums;


public class TruckControll : BaseCar
{
    public Wheel[] wheels;

    Vector3 acceleration;
    Vector3 lastV;
    Vector3 currV;



    [Header("Debug")]

    public float GForce;
    public bool accel = false;
    public bool breaking = false;
    public bool rear = false;


    private Quaternion startRotaion;

    private int curGear = 0; //between 0 - gearscount;
    private float engineTorque;

    
    private float speedbyGear;
    private float speed;


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
        Init(Model.GetCarData(Key),null);
        Rigid.centerOfMass = CenterOfMass.localPosition;

        startRotaion = this.transform.rotation;

        speedbyGear = MaxSpeed/Gears.Length;

        if (wheels.Length <= 0) return;
        if (Gears.Length <= 0) return;
        
        Array.Sort(wheels, CompareCondition);

        radiusSumm = 0;
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

        engineTorque = Gears[curGear] * EngineMaxTorque;
        middleTorq = engineTorque / radiusSumm;

        inited = true;
    }


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
            if ((breaking && !rear) || (accel && Rigid.velocity.x < -1))
            {
                wheels[i].collider.brakeTorque = BrakeForce;
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
            engineTorque = RearGear*EngineMaxTorque;
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
        if (curGear < Gears.Length-1)
        {
            ++curGear;
            engineTorque = Gears[curGear] * EngineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
           // Debug.Log("UP GEAR");
        }
    }

    private void DownGear()
    {
        if (curGear > 0)
        {
            --curGear;
            engineTorque = Gears[curGear] * EngineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
            //Debug.Log("DOWN GEAR");
        }
    }

    private bool isDemo = false;

    public void makeDEMO()
    {
        
        EngineMaxTorque = EngineMaxTorque/2;
        isDemo = true;
        updateGearValues();
    }

    public int getGear()
    {
        return curGear;
    }

   /* public float getTorque()
    {
        return engineTorque;
    }*/

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

        if (Rigid.velocity.x <= 0.1 && breaking)
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
                Rigid.angularVelocity = new Vector3(0, 0, Rigid.angularVelocity.z - AngolarCoef);
            }
            else if (accel)
            {
                Rigid.angularVelocity = new Vector3(0, 0, Rigid.angularVelocity.z + AngolarCoef);
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
        currV = Rigid.velocity;
        acceleration = (currV - lastV)/Time.deltaTime;
        GForce = acceleration.magnitude/9.806f;
    }

    void Update()
    {
        if (!isGrounded())
        {
            if (Rigid.velocity.x > 0)
            {
                Vector3 tmpVel = Rigid.velocity;
                tmpVel.x -= FlySpeedResuce;
                Rigid.velocity = tmpVel;
            }
            Rigid.drag = 0;
        }
        else
        {
            Rigid.drag = drag;
        }

        CarMove();

        if (isDemo)
            MainController.mainCamera.transform.position = new Vector3(transform.position.x + 1,
                transform.position.y+7, -8);
        else
            MainController.mainCamera.transform.position = new Vector3(transform.position.x+3 + Mathf.Abs(Rigid.velocity.x / 2),
                transform.position.y+3+ Mathf.Abs(Rigid.velocity.x/2), -8 - Mathf.Abs(Rigid.velocity.x));

        
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
        tmpRPM = wheelMiddleRPM * Gears[curGear];
        return tmpRPM>MaxEngineRPM? MaxEngineRPM : tmpRPM ;
    }

    private void updateExhaustSystem()
    {
        if (ExhaustSystem.Length == 0)
            return;

        float percent = EngineRPM() / MaxEngineRPM;
        float color = (1 - percent);
        ParticleSystem.MainModule main;
        ParticleSystem.EmissionModule emissionModule;

        float rateOverTime = 50 * percent;
        Color colorC = new Color(color, color, color);
        float startSpeed = 1f + 0.7f * (percent);
        float startSize = 0.3f + (percent);

        for (int i = 0; i < ExhaustSystem.Length; i++)
        {
            main = ExhaustSystem[i].main;
            emissionModule = ExhaustSystem[i].emission;

            emissionModule.rateOverTime = rateOverTime;
            main.startColor = colorC;
            main.startSpeed = startSpeed;
            main.startSize = startSize;
        }
    }
}