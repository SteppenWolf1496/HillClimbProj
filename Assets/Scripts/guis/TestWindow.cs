using UnityEngine.UI;

public class TestWindowParams : WindowShowParameters
{

}

public class TestWindow : BaseWindow
{
    public Text windowNum;
    #region ------------- Window logic -------------

    public static void Show(TestWindowParams prms)
    {
        TestWindow panel = Load("TestWindow") as TestWindow;
        panel.Params.needFader = true;
        panel.Params.allowTouchOutside = false;
        panel.Params.queueType = WindowQueue.Type.New;
        panel.Params.wholeScreen = true;
        panel.InternalShow(prms);
    }

    public override void OnShow()
    {
        base.OnShow();
        windowNum.text = Utility.Format("window#{0}", this.wndOrder.ToString());
    }

    public override void OnHide()
    {
        base.OnHide();
    }

    public override void OnBecameActive()
    {
        base.OnBecameActive();
        windowNum.text = Utility.Format("window#{0}", this.wndOrder.ToString());
    }

    public void OpenTestWnd()
    {
        TestWindow.Show(new TestWindowParams());
    }

    #endregion
}
