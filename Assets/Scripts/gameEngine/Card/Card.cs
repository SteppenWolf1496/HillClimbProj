﻿using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;


public class Card
{
    public ObscuredString key;//ключь
    public List<BaseCarModificator> modifs;
    public string Title;
    public ObscuredInt NeedCollectForLvl; // нужно собрать для лвлапа базово
    public ObscuredFloat NeedCollectForLvlMult; // множитель для каждого последующего уровня
    public ObscuredInt MaxLVL; // максимальный кровень
    public CarModifationRarity ModifationRarity;//рарность



}
