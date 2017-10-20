using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using UnityEngine;

[Serializable]
public class StaticData : MonoBehaviour
{
    [SerializeField] protected ObscuredString key;
    [SerializeField] protected GameObject chestsContainer;

    

    // [SerializeField] protected List<Card> carModificators;

    void Awake()
    {
        LoadStaticCardsData();
        LoadStaticChestsData();
        

    }
    
    
    void Start()
    {
        
    }

    

    private void LoadStaticCardsData()
    {
        Model.Cards.Clear();
        CardEditor[] cards = GetComponentsInChildren<CardEditor>();
        Card tmp;
        foreach (CardEditor card in cards)
        {
            tmp = new Card();
            tmp.Title = card.Title;
            tmp.key = card.key;
            tmp.modifs = card.modifs;
            tmp.MaxLVL = card.MaxLVL;
            tmp.ModifationRarity = card.ModifationRarity;
            tmp.NeedCollectForLvl = card.NeedCollectForLvl;
            tmp.NeedCollectForLvlMult = card.NeedCollectForLvlMult;

            tmp.LvlsWhereDrop = new int[card.LvlsWhereDrop.Length];
            for (int j = 0; j < card.LvlsWhereDrop.Length; j++)
            {
                tmp.LvlsWhereDrop[j] = card.LvlsWhereDrop[j];
            }
            Model.Cards.Add(tmp);
        }
    }

    private void LoadStaticChestsData()
    {
        Model.Chests.Clear();
        ChestEditor[] chests = GetComponentsInChildren<ChestEditor>();
        Chest chest;

        foreach (ChestEditor chestEditor in chests)
        {
            chest = new Chest();
            chest.CommonFrom = chestEditor.CommonFrom;
            chest.CommonTo = chestEditor.CommonFrom;
            chest.EpicFrom = chestEditor.EpicFrom;
            chest.EpicTo = chestEditor.EpicTo;
            chest.GoldFrom = chestEditor.GoldFrom;
            chest.GoldTo = chestEditor.GoldTo;
            chest.Key = chestEditor.Key;
            chest.RareFrom = chestEditor.RareFrom;
            chest.RareTo = chestEditor.RareTo;
            chest.Rarity = chestEditor.Rarity;
            chest.TimeToOpen = chestEditor.TimeToOpen;
            Model.Chests.Add(chest);
        }
    }

    public static int CountCardsByRarity(CarModifationRarity _cardRar, ChestRarity _chestRar)
    {
        int ret = 3 - (int)_cardRar;
        if (_chestRar == ChestRarity.Common)
        {
            ret += (int)UnityEngine.Random.Range(0, 4);
        }
        if (_chestRar == ChestRarity.Rare)
        {
            ret += (int)UnityEngine.Random.Range(0, 2);
        }
        if (_chestRar == ChestRarity.Epic)
        {
            ret += (int)UnityEngine.Random.Range(0, 1);
        }
        return ret;
    }

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */
}
