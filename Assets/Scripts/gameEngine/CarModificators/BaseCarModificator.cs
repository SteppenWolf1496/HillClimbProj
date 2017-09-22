using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

[Serializable]
public class BaseCarModificator
{
    public ObscuredString key;//ключь
    public CarModificatorType ModificatorType;//тип модифа
    //public CarModificationActivation ActivationType;//активный/пассивный
    public ObscuredFloat ModificationValue;//во сколько раз базово повышает
    public ObscuredFloat LvlUpMuliplier;//во сколько раз повышает параметр с уровнем(ModificationValue+ModificationValue*LvlUpMuliplier)
    


    
}
