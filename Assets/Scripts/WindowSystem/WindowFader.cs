using UnityEngine;
using System.Collections;

public class WindowFader : MonoBehaviour
{
    private bool _isHide;
    private bool _isShow = false;

    private bool _isInitParams = false;

    private void Awake()
    {
        InitParams();
    }

    public void Show()
    {
        InitParams();

        if (this._isShow == true) {
            return;
        }
        this._isShow = true;
        this._isHide = false;
    }

    public void Hide()
    {
        InitParams();

        this._isShow = false;
        this._isHide = true;
        OnFadeOutComplete();
    }

    public void OnFadeOutComplete()
    {
        if (this._isHide) {
            gameObject.SetActive(false);
        }
    }

    public void SetAlphaForce(float alpha)
    {
        if (this._isShow == false) {
            return;
        }
    }

    private void InitParams()
    {
        if (_isInitParams == true) {
            return;
        }
        _isInitParams = true;
    }
}
