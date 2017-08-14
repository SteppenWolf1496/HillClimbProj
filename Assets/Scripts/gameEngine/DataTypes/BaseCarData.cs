using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class BaseCarData : MonoBehaviour
{

    
    [SerializeField] public string key;


    [Header("Suspension")]
    [SerializeField] public ObscuredFloat Spring = 90000;
    [SerializeField] public ObscuredFloat Damper = 9000;
    [SerializeField] public ObscuredFloat TargetPosition = 0.5f;

    [Header("Engine")]
    [SerializeField] public ObscuredFloat Torque;
    [SerializeField] public ObscuredInt MaxEngineRPM = 6000;
    [SerializeField] public Rigidbody rigid;
    [SerializeField] public ObscuredFloat engineMaxTorque = 300;
    [SerializeField] public ObscuredFloat brakeForce = 25000;
    [SerializeField] public ObscuredFloat handBrakeForce = 30000;

    [SerializeField] public Transform CenterOfMass;

    [SerializeField] public ObscuredFloat FlySpeedResuce = 0.05f;
    [SerializeField] public ObscuredFloat angolarCoef = 0.1f;

    [SerializeField] public ObscuredFloat[] gears = new ObscuredFloat[5] { 3.6f, 1.95f, 1.357f, 0.941f, 0.784f };
    [SerializeField] public ObscuredFloat rearGear = 3;
    [SerializeField] public ObscuredFloat maxSpeed = 180;

    [SerializeField] public ParticleSystem[] exhaustSystem;


    [Header("Wheels")]
    [SerializeField] public ObscuredFloat ExtremumSlip = 0.4f;
    [SerializeField] public ObscuredFloat ExtremumValue = 1f;
    [SerializeField] public ObscuredFloat AsymptoteSlip = 0.8f;
    [SerializeField] public ObscuredFloat AsymptoteValue = 1f;
    [SerializeField] public ObscuredFloat Stiffness = 1f;
    
}
