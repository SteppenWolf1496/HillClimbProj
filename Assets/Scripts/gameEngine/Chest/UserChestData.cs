using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

public class UserChestData : MonoBehaviour
{
    public ObscuredDouble TimeWhenOpen;
    public ChestRarity Rarity;
    public ObscuredInt Lvl;

    public string GetSaveData()
    {
        return Utility.Format("{0}%{1}%{2}", TimeWhenOpen, Rarity, Lvl);
    }

    public void SetDataFromSave(string _data)
    {
        string[] data = _data.Split('%');

        TimeWhenOpen = int.Parse(data[0]);
        Rarity = (ChestRarity) int.Parse(data[1]);
        Lvl = int.Parse(data[2]);
    }

}
