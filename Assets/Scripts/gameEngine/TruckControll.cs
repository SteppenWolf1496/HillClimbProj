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
    public float GForce;

    public bool accel = false;
    public bool breaking = false;
    public bool rear = false;

    public int MaxEngineRPM = 6000;

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
    private int curGear = 9999; //between 0 - gearscount;
    private float engineTorque;

    public ParticleSystem[] exhaustSystem;
    private float speedbyGear;
    private float speed;

    int CompareCondition(Wheel itemA, Wheel itemB)
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

        rigid.centerOfMass = CenterOfMass.localPosition;

        startPosition = this.transform.position;
        startRotaion = this.transform.rotation;


        speedbyGear = maxSpeed/gears.Length;

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
            if ((breaking && !rear) || (accel && rigid.velocity.x < -1))
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


    private bool inited = false;


    public bool isGrounded()
    {
        bool ret = false;
        foreach (Wheel w in wheels)
        {
            if (w.HasContact)
                ret = true;
        }

        return ret;
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
     
        if (!isGrounded())
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
        //transform.rotation = new Quaternion();
        Vector3 oldRot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, oldRot.z);
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


        CarMove();

        if (isDemo)
            MainController.instance().mainCamera.transform.position = new Vector3(transform.position.x + 1,
                transform.position.y, -8);
        else
            MainController.instance().mainCamera.transform.position = new Vector3(transform.position.x,
                transform.position.y, -8 - (rigid.velocity.x > 0 ? rigid.velocity.x/1.5f : 0));

        
    }

    public float EngineRPM()
    {

        if (wheels.Length<=0)return 0;
        if (gears.Length <= 0 || curGear>gears.Length-1) return 0;
        if (curGear < 0) return wheels[0].RPM *rearGear;
        Debug.Log(wheels[0].RPM * gears[curGear]);
        return wheels[0].RPM * gears[curGear];
    }

    private void updateExhaustSystem()
    {
        if (exhaustSystem.Length == 0)
            return;

        float percent = Mathf.Abs(speed)%speedbyGear/speedbyGear;
        float accelVar = (accel || rear) ? 1 : 0;
        float coef = 0.1f + (accelVar == 0 ? 0 : 1);
        float color = 0.7f - percent* accelVar * 0.5f;

        for (int i = 0; i < exhaustSystem.Length; i++)
        {
            exhaustSystem[i].emissionRate = 70*(coef/*+forceMagnitude*/)
            ;
            exhaustSystem[i].startColor = new Color(color, color, color);
            exhaustSystem[i].startSpeed = 1f + 15f* accelVar;
            exhaustSystem[i].startSize = 0.3f + 1f* accelVar;
        }
    }
}