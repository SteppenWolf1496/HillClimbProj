﻿using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using GameUtility;
using UnityEngine;

public class BaseCar : MonoBehaviour
{

    [SerializeField] protected string key;

    public string Key
    {
        get { return key; }
    }

    [Header("Suspension")]
    private ObscuredFloat spring;
    private ObscuredFloat damper;
    private ObscuredFloat targetPosition;

    [Header("Engine")]
    private ObscuredFloat torque;
    private ObscuredInt maxEngineRPM;
    [SerializeField] private Rigidbody rigid;
    private ObscuredFloat engineMaxTorque;
    private ObscuredFloat brakeForce;
    private ObscuredFloat handBrakeForce;

    [SerializeField] private Transform centerOfMass;

    private ObscuredFloat flySpeedResuce;
    private ObscuredFloat angolarCoef;

    private float[] gears;
    private ObscuredFloat rearGear;
    private ObscuredFloat maxSpeed;

    private ObscuredFloat fuelTank;
    private ObscuredFloat fuelCons;

    [SerializeField] private ParticleSystem[] exhaustSystem;


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

        /*set
        {
            rigid = value;
        }*/
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

    protected float[] Gears
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

        /*set
        {
            exhaustSystem = value;
        }*/
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

    public ObscuredFloat FuelTank
    {
        get
        {
            return fuelTank;
        }

        set
        {
            fuelTank = value;
        }
    }

    public ObscuredFloat FuelCons
    {
        get
        {
            return fuelCons;
        }

        set
        {
            fuelCons = value;
        }
    }

    public void Init(BaseCarData _initData, List<BaseCarModificator> _modifs) 
    {
        Spring = _initData.Spring;
        Damper = _initData.Damper;
        TargetPosition = _initData.TargetPosition;
        Torque = _initData.Torque;
        MaxEngineRPM = _initData.MaxEngineRPM;
        //Rigid = _initData.rigid;
        EngineMaxTorque = _initData.engineMaxTorque;
        BrakeForce = _initData.brakeForce;
        HandBrakeForce = _initData.handBrakeForce;
        //CenterOfMass = _initData.CenterOfMass;
        FlySpeedResuce = _initData.FlySpeedResuce;
        AngolarCoef = _initData.angolarCoef;
        Gears = _initData.gears;
        RearGear = _initData.rearGear;
        MaxSpeed = _initData.maxSpeed;
        //ExhaustSystem = _initData.exhaustSystem;
        ExtremumSlip = _initData.ExtremumSlip;
        ExtremumValue = _initData.ExtremumValue;
        AsymptoteSlip = _initData.AsymptoteSlip;
        AsymptoteValue = _initData.AsymptoteValue;
        Stiffness = _initData.Stiffness;
        FuelTank = _initData.fuelTank;
        FuelCons = _initData.fuelCons;

        if (_modifs == null) return;

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
            case Enums.CarModificatorType.FuelTank:
                break;
            case Enums.CarModificatorType.FuelConsume:
                break;
        }
    }

    private void ApplyEngineTorque(float _value)
    {
        EngineMaxTorque += _value;
    }

    private void ApplyBreakTorcue(float _value)
    {
        BrakeForce += _value;
    }

    private void ApplyCenterMass(Vector3 _dir)
    {
        CenterOfMass.position += _dir;
    }
    private void ApplyGravity(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyRotation(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplySuspentionLift(float _value)
    {
        AngolarCoef += _value;
    }
    private void ApplySuspentionHardness(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyTearsGrip(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyTorquePercentForWd(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyGearBox(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyMaxRPM(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }
    private void ApplyMaxSpeed(float _value)
    {
        Log.Error("ApplyGravity not realized");
    }



}
