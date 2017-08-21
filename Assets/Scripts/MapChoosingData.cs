using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class MapChoosingData: MonoBehaviour
{

    [SerializeField] public ObscuredString Key;

    [SerializeField] public Material Front;
    [SerializeField] public Material Top;
    [SerializeField] public Material Detail;

    [SerializeField] public GameObject Prefub1;
    [SerializeField] public GameObject Prefub2;

    [SerializeField] public ObscuredFloat price;

}
