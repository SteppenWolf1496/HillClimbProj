using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using CodeStage.AntiCheat.ObscuredTypes;
using Enums;
using GameUtility;

public class Model : MonoBehaviour
{




    private const string UCARSKEY = "HC_UCARS";
    private const string METAL = "HC_METAL";
    private const string MONEY = "HC_MONEY";
    private const string CHESTS = "HC_CHESTS";
    private const string CARDS = "HC_CHESTS";

    public static int curCarIndex = 0;
    public static int curMapIndex = 0;
    public static string curMap;
    private static List<BaseCarData> cars = new List<BaseCarData>();
    private static List<UserCarData> ucars = new List<UserCarData>();
    private static List<MapData> maps = new List<MapData>();
    private static List<UserMapData> uMaps = new List<UserMapData>();
    private static List<Card> cards = new List<Card>();
    private static List<UserCard> uCards = new List<UserCard>();

    private static List<Chest> chests = new List<Chest>();
    private static List<UserChestData> uChests = new List<UserChestData>();


    // UserMaps Data
    private static ObscuredInt metal;
    private static ObscuredInt money;

   

    public static List<BaseCarData> BaseCars
    {
        get
        {
            if (cars.Count == 0)
            {
                //ZALIPUHA POKA NETU ADMINKI
                BaseCarData tmp = new BaseCarData();
                tmp.key = "car";
                cars.Add(tmp);
                tmp = new BaseCarData();
                tmp.key = "monster";
                cars.Add(tmp);
            }

            return cars;
        }
       
    }

    public static BaseCarData GetCarData(string _key)
    {
        return BaseCars.Find(x => x.key == _key);
    }

    public static Card GetCardData(string _key)
    {
        return Cards.Find(x => x.key == _key);
    }

    public static List<UserCarData> UserCars
    {
        get
        {
            if (ucars.Count == 0)
            {
                //Base initialisation for user
                UserCarData tmp = new UserCarData();
                tmp.key = "car";
                ucars.Add(tmp);
            }

            return ucars;
        }
    }

    public static List<UserMapData> UserMaps
    {
        get
        {
            if (uMaps.Count == 0)
            {
                UserMapData tmp = new UserMapData();
                tmp.Key = "vilage";
                uMaps.Add(tmp);
                tmp = new UserMapData();
                tmp.Key = "city";
                uMaps.Add(tmp);
            }
            return uMaps;
        }
    }

    public static UserCarData GetUserCarData(string _key)
    {
        UserCarData ret = UserCars.Find(x => x.key == _key);
        
        return ret;
    }

    public static UserCard GetUserCard(string _key)
    {
        UserCard ret = uCards.Find(x => x.key == _key);
        
        return ret;
    }

    public static void AddUserCard(string _key,int _count)
    {
        UserCard card = GetUserCard(_key);
        if (card)
        {
            card.CollectedItems += _count;
        }
        else
        {
            card = new UserCard();
            card.key = _key;
            card.CollectedItems = _count;
            card.Lvl = 0;
            uCards.Add(card);
        }
        
    }

    public static List<Card> Cards
    {
        get
        {
            return cards;
        }
        set { cards = value; }
    }

    public static ObscuredInt Metal
    {
        get
        {
            return metal;
        }

        set
        {
            metal = value;
            SaveMetal();
        }
    }

    public static ObscuredInt Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
            SaveMoney();
        }
    }

    public static List<Chest> Chests
    {
        get
        {
            return chests;
        }

        set
        {
            chests = value;
        }
    }

    public static List<UserChestData> UChests
    {
        get
        {
            return uChests;
        }

        set
        {
            uChests = value;
        }
    }

    public static void SaveUCars()
    {
        string[] toSave = new string[ucars.Count];
        int i = 0;
        foreach (UserCarData ucar in ucars)
        {
            toSave[i] = ucar.GetSave();
            ++i;
        }
        PlayerPrefsElite.SetStringArray(UCARSKEY, toSave);
    }

    public static bool LoadUcars()
    {
        try
        {
            if (PlayerPrefsElite.VerifyArray(UCARSKEY))
            {
                ArrayList data = new ArrayList(PlayerPrefsElite.GetStringArray(UCARSKEY));
                return ProcessLoadUCars(data);
            }
            else
            {
                
                //firstInitData = true;
               
                return true;
            }
        }
        catch (Exception e)
        {
            Log.Error("[SavedDataManager::LoadUserData] EXCEPTION: {0}", e.ToString());
           
            return false;
        }
    }

    public static void SaveMoney()
    {
        PlayerPrefsElite.SetInt(MONEY, Money);
    }

    public static void SaveMetal()
    {
        PlayerPrefsElite.SetInt(METAL, Metal);
    }

    public static bool LoadMoney()
    {
        try
        {
            if (PlayerPrefsElite.VerifyArray(MONEY))
            {
                Money = PlayerPrefsElite.GetInt(MONEY);
                return true;
            }
          
        }
        catch (Exception e)
        {
            Log.Error("[SavedDataManager::LoadUserData] EXCEPTION: {0}", e.ToString());
        }
        return false;
    }
    public static bool LoadMetal()
    {
        try
        {
            if (PlayerPrefsElite.VerifyArray(METAL))
            {
                Metal = PlayerPrefsElite.GetInt(METAL);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Log.Error("[SavedDataManager::LoadUserData] EXCEPTION: {0}", e.ToString());
        }
        return false;
    }

    private static bool ProcessLoadUCars(ArrayList _data)
    {
        ucars.Clear();
        foreach (string s in _data)
        {
           UserCarData tmp = new UserCarData();
           tmp.SetData(s);
           ucars.Add(tmp);
        }

        return true;
    }

    public static void SaveCards()
    {
        string[] toSave = new string[uCards.Count];
        int i = 0;
        foreach (UserCard card in uCards)
        {
            toSave[i] = card.GetSave();
            ++i;
        }
        PlayerPrefsElite.SetStringArray(CARDS, toSave);
    }
    public static bool LoadUCards()
    {
        try
        {
            if (PlayerPrefsElite.VerifyArray(CARDS))
            {
                ArrayList data = new ArrayList(PlayerPrefsElite.GetStringArray(CARDS));
                return ProcessLoadUCards(data);
            }
            else
            {

                //firstInitData = true;

                return true;
            }
        }
        catch (Exception e)
        {
            Log.Error("[SavedDataManager::LoadUCards] EXCEPTION: {0}", e.ToString());

            return false;
        }
    }

    private static bool ProcessLoadUCards(ArrayList _data)
    {
        uCards.Clear();
        foreach (string s in _data)
        {
            UserCard tmp = new UserCard();
            tmp.SetData(s);
            uCards.Add(tmp);
        }

        return true;
    }

    public static void SaveChests()
    {
        string[] toSave = new string[uChests.Count];
        int i = 0;
        foreach (UserChestData uchest in uChests)
        {
            toSave[i] = uchest.GetSaveData();
            ++i;
        }
        PlayerPrefsElite.SetStringArray(CHESTS, toSave);
    }

    public static bool LoadChests()
    {
        try
        {
            if (PlayerPrefsElite.VerifyArray(CHESTS))
            {
                ArrayList data = new ArrayList(PlayerPrefsElite.GetStringArray(CHESTS));
                return ProcessLoadUChests(data);
            }
            else
            {

                return true;
            }
        }
        catch (Exception e)
        {
            Log.Error("[SavedDataManager::LoadUserData] EXCEPTION: {0}", e.ToString());

            return false;
        }
    }

    private static bool ProcessLoadUChests(ArrayList _data)
    {
        UChests.Clear();
        foreach (string s in _data)
        {
            UserChestData tmp = new UserChestData();
            tmp.SetDataFromSave(s);
            UChests.Add(tmp);
        }

        return true;
    }

    public static Chest GetChestData(ChestRarity _rarity)
    {
        return Chests.Find(x => x.Rarity == _rarity);
    }

}

