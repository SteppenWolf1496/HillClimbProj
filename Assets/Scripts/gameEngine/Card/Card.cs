using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;


public class Card
{

    public struct DropCard
    {
        public string key;
        public int count;
    }

    public ObscuredString key;//ключь
    public List<BaseCarModificator> modifs;
    public string Title;
    public ObscuredInt NeedCollectForLvl; // нужно собрать для лвлапа базово
    public ObscuredFloat NeedCollectForLvlMult; // множитель для каждого последующего уровня
    public ObscuredInt MaxLVL; // максимальный уровень
    public CarModifationRarity ModifationRarity;//рарность
    public int[] LvlsWhereDrop; //На каких уровнях(картах) дропается 

    public bool HaveLvl(int _lvl)
    {
        return LvlsWhereDrop.Contains(_lvl);
    }
}
