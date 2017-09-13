using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using CodeStage.AntiCheat.ObscuredTypes;

public class Model : MonoBehaviour
{

    public static int curCarIndex = 0;
    public static int curMapIndex = 0;
    public static string curMap;
    private static List<BaseCarData> cars = new List<BaseCarData>();
    private static List<UserCarData> ucars = new List<UserCarData>();
    private static List<MapData> maps = new List<MapData>();
    private static List<UserMapData> uMaps = new List<UserMapData>();
    private static List<BaseCarModificator> carModifs = new List<BaseCarModificator>();
    private static List<UserCarModificator> uCarmodifs = new List<UserCarModificator>();


    // UserMaps Data
    private static ObscuredInt coins;
    private static ObscuredInt RealMoney;

    public static void AddCoins(int _value)
    {
        coins += _value;
    }

    public static int Coins
    {
        get { return coins; }
    }

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

    public void SaveCar()
    {
        
    }

}

