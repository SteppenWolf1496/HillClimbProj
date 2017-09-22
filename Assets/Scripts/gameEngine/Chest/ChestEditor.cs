using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

public class ChestEditor : MonoBehaviour
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
}
