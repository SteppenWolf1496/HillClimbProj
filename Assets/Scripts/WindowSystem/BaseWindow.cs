using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

	public class WindowShowParameters {}

public class BaseWindow : MonoBehaviour
{
    public sealed class Parameters
    {
        public string prefabName;
        public WindowQueue.Type queueType;
        public bool isModal;
        public float depth;
        public int priority;
        public int layer;
        public bool isDelayed;
        public bool needRelease;
        public bool wholeScreen;
        public WindowController.GameEvent byEvent;
        public bool showOver = false;
        public bool needFader;
        public bool allowTouchOutside;
        public bool unlockInput;
    }

    private Dictionary<KeyCode, Action> _keys = new Dictionary<KeyCode, Action>();
    public int wndOrder;

    public WindowShowParameters ShowParams { get; set; }
    public Parameters Params { get; private set; }
    public bool IsShow { get; set; }

    public RectTransform Panel
    {
        get { return GetComponent<RectTransform>(); }
    }

    protected virtual void Awake()
    {
        Params = new Parameters();
        Params.isModal = false;
        Params.wholeScreen = false;
        Params.needRelease = true;
        Params.needFader = true;
        Params.allowTouchOutside = true;
        Params.unlockInput = false;
    }

    public void Release()
    {
        if (IsShow == true) {
            Hide();
        }
        _keys.Clear();
        transform.parent = null;
        Destroy(gameObject);
    }

    public void Hide()
    {
        WindowController.Get.HideWindow(this);
    }

    public virtual bool isShowAvailable()
    {
        return true;
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    public virtual void OnBecameActive()
    {
    }

    public virtual void OnAddedToStack()
    {
    }

    protected static BaseWindow Load(string prefabName)
    {
        if (string.IsNullOrEmpty(prefabName)) {
            throw new System.ArgumentNullException("Prefab name not set! wtf?");
        }
        GameObject go = WindowController.Get.LoadUI(prefabName);
        return go.GetComponent<BaseWindow>();
    }

    protected void AddInputKey(KeyCode code, Action callback)
    {
        if (_keys.ContainsKey(code) == false) {
            _keys.Add(code, null);
        }
        _keys[code] = callback;
    }

    protected void InternalShow(WindowShowParameters parameters = null)
    {
        if (Params.allowTouchOutside == true) {
            AddInputKey(KeyCode.Escape, () => {
                if (IsShow == false) {
                    return;
                }
                Hide();
            });
        }

        WindowController.Get.ShowWindow(this, parameters);
    }

    protected virtual void Update()
    {
        UpdateKeyInput();
    }

    private void UpdateKeyInput()
    {
        if (WindowController.Get.ActiveWindow != this || this._keys.Count == 0) {
            return;
        }

        foreach (KeyValuePair<KeyCode, Action> it in _keys) {
            if (Input.GetKeyUp(it.Key) == true) {
                it.Value();
                break;
            }
        }
    }
}
