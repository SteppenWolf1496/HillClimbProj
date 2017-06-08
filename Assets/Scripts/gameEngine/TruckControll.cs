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
    private int curGear = 0; //between 0 - gearscount;
    private float engineTorque;

    public ParticleSystem[] exhaustSystem;
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
        
    }

    private float radiusSumm;
    private int notDrive;
    private float middleTorq;
    void Start()
    {
        if (!rigid) rigid = this.GetComponent<Rigidbody>();

        rigid.centerOfMass = CenterOfMass.localPosition;

        startPosition = this.transform.position;
        startRotaion = this.transform.rotation;

        speedbyGear = maxSpeed/gears.Length;


        if (wheels.Length <= 0) return;
        if (gears.Length <= 0) return;
        int i = 0;
        while (i < wheels.Length)
        {
            if (wheels[i].isDrive)
            {
                tmpWheel = wheels[i].collider;
                break;
            }
            ++i;
        }

        Array.Sort(wheels, CompareCondition);

        radiusSumm = 0;
        //Array.Sort(wheels, CompareCondition);
        torqByWheel = new float[wheels.Length];
        notDrive = 0;

        foreach (var t in wheels)
        {
            if (t.isDrive)
                radiusSumm += t.collider.radius;
            else
                notDrive++;
        }

        engineTorque = gears[curGear] * engineMaxTorque;
        middleTorq = engineTorque / radiusSumm;

    }

    public float getSpeed()
    {
        return speed;
    }

    private void countTorque()
    {
        

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

            //	wheels [wheels.Length - i - 1 - notDrive].setWheelDefFreak (ExtremumSlip, ExtremumValue, AsymptoteSlip, AsymptoteValue);
        }

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
        Debug.Log("COUNT GEAR");
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
        Debug.Log("TRY UP GEAR");
        if (curGear < gears.Length-1)
        {
            ++curGear;
            engineTorque = gears[curGear] * engineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
            Debug.Log("UP GEAR");
        }
    }

    private void DownGear()
    {
        if (curGear > 0)
        {
            --curGear;
            engineTorque = gears[curGear] * engineMaxTorque;
            middleTorq = engineTorque / radiusSumm;
            Debug.Log("DOWN GEAR");
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
                transform.position.y+7, -8);
        else
            MainController.instance().mainCamera.transform.position = new Vector3(transform.position.x,
                transform.position.y+3+ (rigid.velocity.x/2), -8 - (rigid.velocity.x));

        
    }

    public WheelCollider tmpWheel = null;
    public float WheelRPM
    {

        get { if (tmpWheel == null) return 0;
            return tmpWheel.rpm;
        }
        
}

    private float tmpRPM = 0;
    public float EngineRPM()
    {
        tmpRPM = WheelRPM * gears[curGear];
        return tmpRPM>MaxEngineRPM? MaxEngineRPM : tmpRPM ;
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
            exhaustSystem[i].emissionRate = 7*(coef/*+forceMagnitude*/)
            ;
            exhaustSystem[i].startColor = new Color(color, color, color);
            exhaustSystem[i].startSpeed = 1f + 0.5f* accelVar;
            exhaustSystem[i].startSize = 0.3f + 1f* accelVar;
        }
    }
}