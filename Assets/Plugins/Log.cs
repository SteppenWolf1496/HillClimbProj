using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace GameUtility
{
    


public class Log : MonoBehaviour {

    public static void Error(string _message)
    {
        Log.Error(_message);
    }

    public static void Warning(string _message)
    {
        Debug.LogWarning(_message);
    }
    public static void Temp(string _message)
    {
        Debug.Log(_message);
    }
    public static void Temp(StringBuilder _message)
    {
        Debug.Log(_message);
    }

    public static void Exception(Exception _message)
    {
        Debug.LogException(_message);
        
    }

    public static void Error(string format, params object[] args)
    {
       
        Log.Error(Utility.Format(format, args));
    }

    public static void Warning(string format, params object[] args)
    {
        Debug.LogWarning(Utility.Format(format, args));
    }
    public static void Temp(string format, params object[] args)
    {
        Debug.Log(Utility.Format(format, args));
    }
}
}
