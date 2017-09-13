using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

[Serializable]
public class BaseCarModificator
{
    public ObscuredString key;
    public CarModificatorType ModificatorType;
    public CarModificationActivation ActivationType;
    public CarModifationRarity ModifationRarity;
    public ObscuredInt MoficatorLvl;
    public ObscuredFloat ModificationValue;
    public ObscuredFloat LvlUpMuliplier;
    public string Title;


    
}
