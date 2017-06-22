using System.Collections;
using System.Collections.Generic;
using SSC;
using UnityEngine;

public class SingletonePattern<T> : MonoBehaviour where T : MonoBehaviour
{
    
   
   
        
        private static T instance;

        public static T Instance
        {
            get
            {
            if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        obj.hideFlags = HideFlags.DontSave;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
        }
        }
   
}
