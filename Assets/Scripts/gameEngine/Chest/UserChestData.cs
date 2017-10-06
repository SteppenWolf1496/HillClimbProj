using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

public class UserChestData
{
    public ObscuredInt TimeWhenOpen;
    public int TimeWhenAdd;
    public ChestRarity Rarity;
    public ObscuredInt Lvl;
    public ChestState state;

    public string GetSaveData()
    {
        return Utility.Format("{0}%{1}%{2}%{3}%{4}", TimeWhenOpen, (int)Rarity, Lvl, (int)state, TimeWhenAdd);
    }

    public void SetDataFromSave(string _data)
    {
        string[] data = _data.Split('%');

        TimeWhenOpen = int.Parse(data[0]);
        Rarity = (ChestRarity) int.Parse(data[1]);
        Lvl = int.Parse(data[2]);
        state = (ChestState)int.Parse(data[3]);
        TimeWhenAdd = int.Parse(data[4]);
    }

}
