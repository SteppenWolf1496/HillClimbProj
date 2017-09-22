using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class UserCard : MonoBehaviour
{

    public ObscuredString key;
    public ObscuredInt CollectedItems;
    public ObscuredInt Lvl;


    public string GetSave()
    {
        return Utility.Format("{0}%{1}%{2}", key, CollectedItems, Lvl);
    }

    public void SetData(string _data)
    {
        string[] data = _data.Split('%');
        key = data[0];
        CollectedItems = int.Parse(data[1]);
        Lvl = int.Parse(data[2]);

    }
}
