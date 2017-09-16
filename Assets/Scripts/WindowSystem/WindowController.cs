using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class WindowController
{
    public enum GameEvent
    {
        UNUSED = 0,
        SHOW_PREV_WINDOW = 1,
        IN_LOBBY = 2,
        IN_FIGHT = 3,
        IN_KILL_CAM = 4,
    }

    private static WindowController _instance;
    private static readonly int _defaultDepth = 100;

    private WindowFader _fader;
    private WindowQueue.Item _activeWindow;
    private List<BaseWindow> _showWindows = new List<BaseWindow>();
    private List<WindowQueue.Item> _stackWindows = new List<WindowQueue.Item>();
    private List<WindowQueue> _queues = new List<WindowQueue>();

    #region --------- Property -----------------

    public static WindowController Get
    {
        get {
            if (_instance == null) {
                _instance = new WindowController();
            }
            return _instance;
        }
    }

    public WindowFader Fader
    {
        get {
            if (_fader == null) {
                _fader = LoadUI("Fader").GetComponent<WindowFader>();
            }
            return _fader;
        }
    }

    public BaseWindow ActiveWindow
    {
        get { return _activeWindow == null ? null : _activeWindow.Window; }
    }

    public int ShowWindowsCount
    {
        get { return _showWindows.Count; }
    }

    #endregion

    #region --------- Load UI ----------

    private Dictionary<string, GameObject> _uiCache = new Dictionary<string, GameObject>();

    public GameObject LoadUI(string prefabName, bool isActive = false)
    {
        string prefabPath = string.Format("UI/Windows/{0}", prefabName);
        GameObject prefab = LoadUIPrefab(prefabPath);
        GameObject inst = AddChild(CanvasController.MainCanvas.gameObject, prefab);
        inst.name = string.Format("{0}", prefabName);
        inst.gameObject.SetActive(isActive);
        return inst;
    }

    private GameObject LoadUIPrefab(string prefabPath)
    {
        GameObject prefab = null;

        if (_uiCache.ContainsKey(prefabPath)) {
            prefab = _uiCache[prefabPath];
        }
        else {
            prefab = Resources.Load(prefabPath) as GameObject;
            _uiCache.Add(prefabPath, prefab);
        }

        return prefab;
    }

    public static GameObject AddChild(GameObject parent, GameObject prefab)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        if (go != null && parent != null) {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
        return go;
    }

    #endregion

    #region --------- Public interface -----------------

    private int wndNum;

    public void ShowWindow(BaseWindow window, WindowShowParameters parameters = null)
    {
        window.wndOrder = wndNum;
        if (window.Params.byEvent != GameEvent.UNUSED) {
            AddToStack(window, parameters);
            CheckShow();
            return;
        }
        AddToQueue(window, parameters);
        ShowActive();
        wndNum++;
    }

    public void HideWindow(BaseWindow window)
    {
        if (window == null || _activeWindow == null) {
            return;
        }
        bool closeNoActiveWindow = window != _activeWindow.Window;

        BaseWindow prevActiveWnd = _activeWindow.Window;
        for (int i = 0; i < _queues.Count; ++i) {
            WindowQueue queue = _queues[i];
            if (queue.IsInQueue(window)) {
                _showWindows.Remove(window);

                window.IsShow = false;
                window.gameObject.SetActive(false);
                window.OnHide();

                queue.Remove(window);
                if (window.Params.needRelease == true) {
                    window.Release();
                }
                break;
            }
        }
        if (!closeNoActiveWindow) {
            _activeWindow = null;
        }
        CheckQueues();

        if (closeNoActiveWindow) {
            return;
        }

        CheckShow();

        UpdateDepth();

        if (_activeWindow != null && _activeWindow.Window != prevActiveWnd) {
            _activeWindow.Window.OnBecameActive();
        }

        UpdateVisibleInvisibleStuff();

        /*if(_showWindows.Count == 0) {
				this.Dispatch(EventType.HIDE_LAST_WINDOW);
			}*/

        wndNum--;
    }

    public void HideActiveWindow()
    {
        if (_activeWindow == null) {
            return;
        }
        HideWindow(_activeWindow.Window);
    }

    public void ForceHideAllWindow()
    {
        HideAllWindow(false, true);
    }

    public void HideAllWindow(bool onlyIsNoModal = true, bool isClearStackDefferedWindow = false)
    {
        while (_activeWindow != null && ((_activeWindow.Window.Params.isModal == false && onlyIsNoModal) || !onlyIsNoModal)) {
            HideActiveWindow();
        }

        if (isClearStackDefferedWindow == true && _stackWindows != null) {
            ClearStack();
        }
    }

    public void DispatchEvent(GameEvent gameEvent)
    {
        CheckShow(CheckStack(gameEvent));
    }

    #endregion

    #region --------- Privete interface -----------------

    private WindowController()
    {
        this._queues.Add(new WindowQueue());
        wndNum = 0;
    }

    private void ClearStack()
    {
        for (int i = 0; i < _stackWindows.Count; i++) {
            _stackWindows[i].Window.OnHide();
            _stackWindows[i].Window.Release();
        }
        _stackWindows.Clear();
    }

    private void AddToStack(BaseWindow window, WindowShowParameters parameters)
    {
        window.Params.isDelayed = true;
        window.OnAddedToStack();
        _stackWindows.Add(new WindowQueue.Item(window, parameters));
    }

    private bool CheckStack(GameEvent gameEvent)
    {
        int i = 0;
        bool showOver = false;
        bool seted = false;
        WindowQueue.Item item = null;
        while (i < _stackWindows.Count) {
            item = _stackWindows[i];
            if (item.Window.Params.byEvent == gameEvent) {
                if (seted == false) {
                    seted = true;
                    showOver = item.Window.Params.showOver;
                }
                item.Window.Params.isDelayed = false;
                AddToQueue(item.Window, item.ShowParams);
                _stackWindows.RemoveAt(i);
            }
            else {
                ++i;
            }
        }
        return showOver;
    }

    private void AddToQueue(BaseWindow window, WindowShowParameters parameters = null)
    {
        WindowQueue queue = null;

        switch (window.Params.queueType) {
            case WindowQueue.Type.Default:
                queue = _queues[0];
                break;
            case WindowQueue.Type.New:
                queue = new WindowQueue();
                _queues.Add(queue);
                break;
            case WindowQueue.Type.Top:
                queue = _queues[_queues.Count - 1];
                break;
        }

        queue.Add(window, parameters);
    }

    private void CheckQueues()
    {
        int queueIndex = 0;

        for (int i = 1; i < _queues.Count; ++i) {
            if (_queues[i].Count == 0) {
                queueIndex = i;
                break;
            }
        }

        if (queueIndex > 0) {
            _queues.RemoveAt(queueIndex);
        }
    }

    private void UpdateDepth()
    {
        if (_showWindows.Count == 0) {
            Fader.Hide();
            return;
        }

        if (ActiveWindow.Params.needFader) {
            Vector3 pos = Fader.GetComponent<RectTransform>().anchoredPosition3D;
            pos.z = ActiveWindow.Panel.anchoredPosition3D.z + 1;
            Fader.GetComponent<RectTransform>().anchoredPosition3D = pos;
            Fader.Show();
        }
        else {
            Fader.Hide();
        }
    }

    private void CheckShow(bool showOver = false)
    {
        if (showOver == false) {
            if (_activeWindow != null) {
                return;
            }
        }
        for (int i = _queues.Count; i != 0; --i) {
            _activeWindow = _queues[i - 1].GetLastItem();
            if (_activeWindow != null) {
                ShowActive(false);
                return;
            }
        }
    }

    private void ShowActive(bool newWindow = true)
    {
        if (newWindow) {
            for (int i = _queues.Count; i != 0; --i) {
                _activeWindow = _queues[i - 1].GetLastItem();
                if (_activeWindow != null) {
                    break;
                }
            }
        }

        if (_activeWindow != null && _activeWindow.Window.IsShow == false) {
            _showWindows.Add(_activeWindow.Window);
        }

        //_nextDepth = _defaultDepth;

        UpdateDepth();

        UpdateVisibleInvisibleStuff();

        if (_activeWindow != null) {
            if (_activeWindow.Window.IsShow == false) {
                _activeWindow.Window.IsShow = true;
                _activeWindow.Window.gameObject.SetActive(true);
                _activeWindow.Window.ShowParams = _activeWindow.ShowParams;
                _activeWindow.Window.OnShow();

                //if(_showWindows.Count == 1) {
                //    this.Dispatch(EventType.SHOW_FIRST_WINDOW);
                //}
            }
        }
    }

    private void UpdateVisibleInvisibleStuff()
    {
        if (_activeWindow != null && _activeWindow.Window.Params.wholeScreen) {
            SetVisibleNotActiveStuff(false);
        }
        else if (IsWholeScreenInShownWindows()) {
            SetVisibleNotActiveStuff(true);
        }
        else {
            SetVisibleNotActiveStuff(true);
        }
    }

    private bool IsWholeScreenInShownWindows()
    {
        for (int i = 0; i < _showWindows.Count; ++i) {
            if (_showWindows[i].Params.wholeScreen) {
                return true;
            }
        }
        return false;
    }

    private void SetVisibleNotActiveStuff(bool visible)
    {
        BaseWindow window = null;
        RectTransform windowUIPanel = null;
        BaseWindow activeWindow = ActiveWindow;
        for (int i = 0; i < _showWindows.Count; ++i) {
            window = _showWindows[i];
            if (window == activeWindow) {
                continue;
            }
            if (window == null) {
                continue;
            }
            windowUIPanel = window.GetComponent<RectTransform>();
            if (windowUIPanel == null) {
                continue;
            }
            windowUIPanel.gameObject.SetActive(visible);
        }
        if (activeWindow != null) {
            windowUIPanel = activeWindow.GetComponent<RectTransform>();
            if (windowUIPanel != null) {
                windowUIPanel.gameObject.SetActive(true);
            }
        }
        if (_showWindows.Count > 0) {
            Fader.SetAlphaForce(visible ? 0.75f : 0f);
        }
    }
}

#endregion
