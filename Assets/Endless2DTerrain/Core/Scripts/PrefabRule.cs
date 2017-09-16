using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Endless2DTerrain
{
    [Serializable]
    public class PrefabRule
    {

        public PrefabRule()
        {
            LastPrefabLocation = Vector3.zero;
            CurrentLocation = Vector3.zero;
            StartLocation = Vector3.zero;
        }

        //User will adjust these settings
        public Vector3 Offset;
        public GameObject PrefabToClone;
        public float MinRepeatDistance;
        public float MaxRepeatDistance;
        public float MinMaxEncreasing = 0;
        [HideInInspector]
        private int usingCount = 0;

        public int MinGroupSize;
        public int MaxGroupSize;

        public float MinGroupSpacing;
        public float MaxGroupSpacing;

        public float MinSlope;
        public float MaxSlope;

        public bool MatchGroundAngle;

        //Use for tracking prefabs and setting their location
        public Vector3 StartLocation{get;set;}
        public Vector3 CurrentLocation { get; set; }
        public Vector3 LastPrefabLocation { get; set; }

        public bool UseMinDistance;
        public float MinDistance;
        public bool UseMaxDistance;
        public float MaxDistance;

        



        public void InstantiatePrefab(Vector3 position, GameObject prefabManager, PrefabPool pool, float angle)
        {
            var prefab = pool.Add(PrefabToClone, position, angle, PrefabToClone.name, MatchGroundAngle);            
            prefab.transform.parent = prefabManager.transform;
            if (MinMaxEncreasing != 0)
            {
                UsingCount++;
               // Log.Temp("usingCount = " + UsingCount);
            }
            //If we have an offset (and we are placing prefabs at an angle), get the direction of that offset.
            //In otherwords, if our offset says to move one up in the y direction, getting the transform direction means the 
            //prefab will move one up relative to the rotation it currently has
            Vector3 transformDirection = prefab.transform.TransformDirection(Offset);
            prefab.transform.position = transformDirection + prefab.transform.position;
        }



        public bool AddPrefab(float repeatDistance)
        {
           
			if (CurrentLocation.x > LastPrefabLocation.x){
				return Mathf.Abs(CurrentLocation.x - LastPrefabLocation.x) >= repeatDistance;
			}
            return false;
       
        }

        public float NextPrefabXLocation(float repeatDistance)
        {
            return (LastPrefabLocation.x + repeatDistance);
        }

        public float GroupSpacing
        {
            get
            {
                if (MinGroupSpacing == MaxGroupSpacing) { return MaxGroupSpacing; }
                return UnityEngine.Random.Range(MinGroupSpacing, MaxGroupSpacing);
            }
        }

        public int GroupSize
        {
            get
            {
                if (MinGroupSize == MaxGroupSize) { return MaxGroupSize; }
                return UnityEngine.Random.Range(MinGroupSize, MaxGroupSize);
            }
        }

        public float RepeatDistance
        {
            get
            {
                if (MinMaxEncreasing == 0)
                {
                    if (MinRepeatDistance == MaxRepeatDistance)
                    {
                        return MaxRepeatDistance;
                    }
                    return UnityEngine.Random.Range(MinRepeatDistance, MaxRepeatDistance);
                }
                else
                {
                    //Log.Temp("PrefabRule::RepeatDistance usingCount =  "+ UsingCount);
                    return MaxRepeatDistance + MinMaxEncreasing * UsingCount;
                }
            }
        }

        public float DistanceTraveled
        {
            get
            {
                return CurrentLocation.x - StartLocation.x;
            }
        }

        public int UsingCount
        {
            get
            {
                return usingCount;
            }

            set
            {
                usingCount = value;
            }
        }
    }

}
