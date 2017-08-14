using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class BaseCar : MonoBehaviour
{


    [Header("Suspension")]
    private ObscuredFloat spring;
    private ObscuredFloat damper;
    private ObscuredFloat targetPosition;

    [Header("Engine")]
    private ObscuredFloat torque;
    private ObscuredInt maxEngineRPM;
    private Rigidbody rigid;
    private ObscuredFloat engineMaxTorque;
    private ObscuredFloat brakeForce;
    private ObscuredFloat handBrakeForce;

    private Transform centerOfMass;

    private ObscuredFloat flySpeedResuce;
    private ObscuredFloat angolarCoef;

    private ObscuredFloat[] gears;
    private ObscuredFloat rearGear;
    private ObscuredFloat maxSpeed;

    private ParticleSystem[] exhaustSystem;


    [Header("Wheels")]
    private ObscuredFloat extremumSlip;
    private ObscuredFloat extremumValue;
    private ObscuredFloat asymptoteSlip;
    private ObscuredFloat asymptoteValue;
    private ObscuredFloat stiffness;

    protected ObscuredFloat Spring
    {
        get
        {
            return spring;
        }

        set
        {
            spring = value;
        }
    }

    protected ObscuredFloat Damper
    {
        get
        {
            return damper;
        }

        set
        {
            damper = value;
        }
    }

    protected ObscuredFloat TargetPosition
    {
        get
        {
            return targetPosition;
        }

        set
        {
            targetPosition = value;
        }
    }

    protected ObscuredFloat Torque
    {
        get
        {
            return torque;
        }

        set
        {
            torque = value;
        }
    }

    protected ObscuredInt MaxEngineRPM
    {
        get
        {
            return maxEngineRPM;
        }

        set
        {
            maxEngineRPM = value;
        }
    }

    protected Rigidbody Rigid
    {
        get
        {
            return rigid;
        }

        set
        {
            rigid = value;
        }
    }

    protected ObscuredFloat EngineMaxTorque
    {
        get
        {
            return engineMaxTorque;
        }

        set
        {
            engineMaxTorque = value;
        }
    }

    protected ObscuredFloat BrakeForce
    {
        get
        {
            return brakeForce;
        }

        set
        {
            brakeForce = value;
        }
    }

    protected ObscuredFloat HandBrakeForce
    {
        get
        {
            return handBrakeForce;
        }

        set
        {
            handBrakeForce = value;
        }
    }

    protected Transform CenterOfMass
    {
        get
        {
            return centerOfMass;
        }

        set
        {
            centerOfMass = value;
        }
    }

    protected ObscuredFloat FlySpeedResuce
    {
        get
        {
            return flySpeedResuce;
        }

        set
        {
            flySpeedResuce = value;
        }
    }

    protected ObscuredFloat AngolarCoef
    {
        get
        {
            return angolarCoef;
        }

        set
        {
            angolarCoef = value;
        }
    }

    protected ObscuredFloat[] Gears
    {
        get
        {
            return gears;
        }

        set
        {
            gears = value;
        }
    }

    protected ObscuredFloat RearGear
    {
        get
        {
            return rearGear;
        }

        set
        {
            rearGear = value;
        }
    }

    protected ObscuredFloat MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    protected ParticleSystem[] ExhaustSystem
    {
        get
        {
            return exhaustSystem;
        }

        set
        {
            exhaustSystem = value;
        }
    }

    protected ObscuredFloat ExtremumSlip
    {
        get
        {
            return extremumSlip;
        }

        set
        {
            extremumSlip = value;
        }
    }

    protected ObscuredFloat ExtremumValue
    {
        get
        {
            return extremumValue;
        }

        set
        {
            extremumValue = value;
        }
    }

    protected ObscuredFloat AsymptoteSlip
    {
        get
        {
            return asymptoteSlip;
        }

        set
        {
            asymptoteSlip = value;
        }
    }

    protected ObscuredFloat AsymptoteValue
    {
        get
        {
            return asymptoteValue;
        }

        set
        {
            asymptoteValue = value;
        }
    }

    protected ObscuredFloat Stiffness
    {
        get
        {
            return stiffness;
        }

        set
        {
            stiffness = value;
        }
    }

    public void Init(BaseCarData _initData, List<BaseCarModificator> _modifs) 
    {
        Spring = _initData.Spring;
        Damper = _initData.Damper;
        TargetPosition = _initData.TargetPosition;
        Torque = _initData.Torque;
        MaxEngineRPM = _initData.MaxEngineRPM;
        Rigid = _initData.rigid;
        EngineMaxTorque = _initData.engineMaxTorque;
        BrakeForce = _initData.brakeForce;
        HandBrakeForce = _initData.handBrakeForce;
        CenterOfMass = _initData.CenterOfMass;
        FlySpeedResuce = _initData.FlySpeedResuce;
        AngolarCoef = _initData.angolarCoef;
        Gears = _initData.gears;
        RearGear = _initData.rearGear;
        MaxSpeed = _initData.maxSpeed;
        ExhaustSystem = _initData.exhaustSystem;
        ExtremumSlip = _initData.ExtremumSlip;
        ExtremumValue = _initData.ExtremumValue;
        AsymptoteSlip = _initData.AsymptoteSlip;
        AsymptoteValue = _initData.AsymptoteValue;
        Stiffness = _initData.Stiffness;

        foreach (var mod in _modifs)
        {
            ApplyModificator(mod);
        }

    }

    private void ApplyModificator(BaseCarModificator _modificator)
    {
        switch (_modificator.ModificatorType)
        {
            case Enums.CarModificatorType.EngineTorque:
                break;
            case Enums.CarModificatorType.BreakTorcue:
                break;
            case Enums.CarModificatorType.CenterMass:
                break;
            case Enums.CarModificatorType.Gravity:
                break;
            case Enums.CarModificatorType.Rotation:
                break;
            case Enums.CarModificatorType.SuspentionLift:
                break;
            case Enums.CarModificatorType.SuspentionHardness:
                break;
            case Enums.CarModificatorType.TearsGrip:
                break;
            case Enums.CarModificatorType.TorquePercentForWd:
                break;
            case Enums.CarModificatorType.GearBox:
                break;
            case Enums.CarModificatorType.MaxRPM:
                break;
            case Enums.CarModificatorType.MaxSpeed:
                break;
        }
    }


}
