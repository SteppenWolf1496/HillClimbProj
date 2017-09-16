using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotifyMGR : MonoBehaviour {

    public enum invokeType
    {
        onShow,
        onHide,
        both
    }
    public float timeOut = 3f;
    public float animTimePart = 0.3f;
    float time;
    float showTime;
    public bool animTransparency = false;
    public invokeType InvokeType = invokeType.onShow;
    public Transform spawnPoint;
    public Transform prefab;
    public AnimationClip animShow;
    public AnimationClip animHide;

    

    Transform notify;
    Text text;
    RawImage img;
    Animation anima;
    bool hidden = false;

    // Use this for initialization
    void Start () {
	}

    

	// Update is called once per frame
	void Update () {
        showTime = timeOut * animTimePart;
        
        if (NotifyData.count > 0)
        {
            if (text != null && NotifyData.current.text != "New Text")
            {
                text.text = NotifyData.current.text;
                if (animTransparency)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, (hidden)?(1f * (time / (showTime))):(1f * ((timeOut - time)/ showTime)));
                }
            }
            if (img != null && NotifyData.currentIcon != Texture2D.whiteTexture)
            {
                img.texture = NotifyData.currentIcon;
                img.gameObject.SetActive(true);
                if (animTransparency)
                {
                    img.color = new Color(img.color.r, img.color.g, img.color.b, (hidden) ? (1f * (time / (showTime))) : (1f * ((timeOut - time) / showTime)));
                }
            }
            else
            {
                try { img.gameObject.SetActive(false); }
                catch { }
            }
        }
        
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= showTime)
            {
                if (!hidden)
                {
                    if (animHide != null)
                    {
                        if (!notify.gameObject.GetComponent<Animation>())
                        {
                            anima = notify.gameObject.AddComponent<Animation>();
                        }
                        else
                        {
                            anima = notify.gameObject.GetComponent<Animation>();
                        }
                        animHide.legacy = true;
                        anima.AddClip(animHide, animHide.name);
                        anima.clip = animHide;
                        anima.playAutomatically = false;
                        //Log.Temp(animHide.name);
                        anima[animHide.name].speed = 1 / showTime;
                        anima.Play(animHide.name);
                        
                    }
                    if (InvokeType == invokeType.onHide || (InvokeType == invokeType.both && NotifyData.current.act2 == null))
                    {
                        NotifyData.current.InvokeAction();
                    }
                    if (InvokeType == invokeType.both && NotifyData.current.act2 != null)
                    {
                        NotifyData.current.InvokeAction2();
                    }
                    hidden = true;
                }
            }
        }
        else
        {
            if (NotifyData.count > 0)
            {
                if (notify != null)
                {
                    Destroy(notify.gameObject);
                }
                
                NotifyData.RemoveFirst();
                if (NotifyData.count > 0)
                {
                    hidden = false;
                    time = timeOut;
                    notify = Instantiate(prefab);
                    notify.SetParent(spawnPoint);
                    notify.localPosition = new Vector3();
                    if (spawnPoint.transform is RectTransform)
                    {
                        notify.localPosition = new Vector3((spawnPoint as RectTransform).sizeDelta.x, (spawnPoint as RectTransform).offsetMin.y);
                    }
                    notify.localScale = new Vector3(1,1,1);
                    text = notify.GetComponentInChildren<Text>(true);
                    img = notify.GetComponentInChildren<RawImage>(true);
                    if (InvokeType == invokeType.onShow || (InvokeType == invokeType.both && NotifyData.current.act != null))
                    {
                        NotifyData.current.InvokeAction();
                    }
                    if (animShow != null)
                    {
                        
                        if (!notify.gameObject.GetComponent<Animation>())
                        {
                            anima = notify.gameObject.AddComponent<Animation>();
                        }
                        else
                        {
                            anima = notify.gameObject.GetComponent<Animation>();
                        }
                        animShow.legacy = true;
                        anima.AddClip(animShow, animShow.name);
                        anima.clip = animShow;
                        anima.playAutomatically = false;
                        //Log.Temp(animShow.name);
                        anima[animShow.name].speed = 1 / showTime;
                        anima.Play(animShow.name);
                    }
                    

                }
                
            }
            else
            {
                if (notify != null)
                {
                    Destroy(notify.gameObject);
                }
            }
            
        }
	}
}
