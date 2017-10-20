using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using Enums;

[Serializable]
public class CardEditor : MonoBehaviour
{
    public ObscuredString key;//ключь
    [SerializeField] public List<BaseCarModificator> modifs;
    public string Title;
    public ObscuredInt NeedCollectForLvl; // нужно собрать для лвлапа базово
    public ObscuredFloat NeedCollectForLvlMult; // множитель для каждого последующего уровня
    public ObscuredInt MaxLVL; // максимальный кровень
    public CarModifationRarity ModifationRarity;//рарность
    public ObscuredInt[] LvlsWhereDrop; //На каких уровнях(картах) дропается 


}
