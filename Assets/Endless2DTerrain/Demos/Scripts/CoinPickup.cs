using UnityEngine;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using Endless2DTerrain;

public class CoinPickup : MonoBehaviour
{
    private ObscuredInt metal = 10;
    private Animation anim;
    private Transform insideObj;

    void OnEnable()
    {
        if (insideObj)
            insideObj.localPosition = Vector3.zero;
    }

    void OnTriggerEnter(Collider item)
    {

        if (anim == null)
        {
            anim = GetComponent<Animation>();
        }
        
        if (insideObj == null)
        {
            insideObj = transform.GetChild(0);
        }
        anim.Play();
        
        Model.Metal += metal;
        /* //Assume only one terrain displayer at a time
         var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;        
         if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
         {
             terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
         }*/


    }



    public void AnimationComplete()
    {
        insideObj.localPosition = Vector3.zero;
        
        var terrainDisplayer = GameObject.FindObjectOfType(typeof(TerrainDisplayer)) as TerrainDisplayer;
        if (terrainDisplayer != null && terrainDisplayer.PrefabManager != null && terrainDisplayer.PrefabManager.Pool != null)
        {
            terrainDisplayer.PrefabManager.Pool.Remove(this.gameObject);
        }
    }

}
