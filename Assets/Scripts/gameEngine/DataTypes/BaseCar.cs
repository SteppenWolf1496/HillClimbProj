using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class BaseCar : MonoBehaviour
{

    
    [SerializeField] protected string key;


    [Header("Suspension")]
    [SerializeField] protected ObscuredFloat Spring = 90000;
    [SerializeField] protected ObscuredFloat Damper = 9000;
    [SerializeField] protected ObscuredFloat TargetPosition = 0.5f;

    [Header("Engine")]
    [SerializeField]
    protected ObscuredFloat Torque;
    [SerializeField] protected ObscuredInt MaxEngineRPM = 6000;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected ObscuredFloat engineMaxTorque = 300;
    [SerializeField] protected ObscuredFloat brakeForce = 25000;
    [SerializeField] protected ObscuredFloat handBrakeForce = 30000;

    [SerializeField] protected Transform CenterOfMass;

    [SerializeField] protected ObscuredFloat FlySpeedResuce = 0.05f;
    [SerializeField] protected ObscuredFloat angolarCoef = 0.1f;

    [SerializeField] protected ObscuredFloat[] gears = new ObscuredFloat[5] { 3.6f, 1.95f, 1.357f, 0.941f, 0.784f };
    [SerializeField] protected ObscuredFloat rearGear = 3;
    [SerializeField] protected ObscuredFloat maxSpeed = 180;
   

    [Header("Wheels")]
    [SerializeField] protected ObscuredFloat ExtremumSlip = 0.4f;
    [SerializeField] protected ObscuredFloat ExtremumValue = 1f;
    [SerializeField] protected ObscuredFloat AsymptoteSlip = 0.8f;
    [SerializeField] protected ObscuredFloat AsymptoteValue = 1f;
    [SerializeField] protected ObscuredFloat Stiffness = 1f;

    
}
