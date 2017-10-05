using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
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

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */
}
