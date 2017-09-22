using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DigitalRuby.Tween;

public class ProgressBar : MonoBehaviour
{

    [SerializeField] public Image foregroundImage;

    public int Value
    {
        get
        {
            if (foregroundImage != null)
                return (int) (foregroundImage.fillAmount * 100);
            else
                return 0;
        }
        set
        {
            if (foregroundImage != null)
                foregroundImage.fillAmount = value / 100f;
        }
    }

    void Start()
    {
       // foregroundImage = gameObject.GetComponent<Image>();
        Value = 0;
    }

//Testing: this function will be called when Test Button is clicked
    public void UpdateProgress()
    {
        
        TweenFactory.Tween("progress", 0f, 100f, 3f, TweenScaleFunctions.QuinticEaseIn, null);
        //iTween.ValueTo(gameObject, param);
    }

    public void TweenedSomeValue(int val)
    {
        Value = val;
    }

    public void OnFullProgress()
    {
        Value = 0;
    }
}