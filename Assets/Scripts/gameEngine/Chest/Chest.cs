using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

public class Chest 
{

    public string Key;
    public ChestRarity Rarity;
    public ObscuredInt GoldFrom;
    public ObscuredInt GoldTo;

    public ObscuredInt CommonFrom;
    public ObscuredInt CommonTo;

    public ObscuredInt RareFrom;
    public ObscuredInt RareTo;

    public ObscuredInt EpicFrom;
    public ObscuredInt EpicTo;

    public ObscuredInt TotalCards;

    public ObscuredInt TimeToOpen;

    public int GetGold(int _lvl)
    {
        float ret = Random.Range(GoldFrom, GoldTo);
        ret += ret * (0.1f * _lvl);
        return (int)ret;
    }

    public int GetCommonCount(int _lvl,int _max)
    {
        if (_max > CommonTo)
        {
            _max = CommonTo;
        }
        float ret = Random.Range(CommonFrom, _max);
        ret += ret * (0.1f * _lvl);
        return (int)ret;
    }

    public int GetRareCount(int _lvl, int _max)
    {
        if (_max > RareTo)
        {
            _max = RareTo;
        }
        float ret = Random.Range(RareFrom, _max);
        ret += ret * (0.1f * _lvl);
        return (int)ret;
    }

    public int GetEpicCount(int _lvl, int _max)
    {
        if (_max > EpicTo)
        {
            _max = EpicTo;
        }
        float ret = Random.Range(EpicFrom, _max);
        ret += ret * (0.1f * _lvl);
        return (int)ret;
    }

}
