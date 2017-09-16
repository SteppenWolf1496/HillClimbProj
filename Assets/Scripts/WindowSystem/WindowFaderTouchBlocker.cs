using UnityEngine;
using System.Collections;

public class WindowFaderTouchBlocker : MonoBehaviour
{
    private void OnClick()
    {
        BaseWindow window = WindowController.Get.ActiveWindow;
        if (window != null && window.Params.allowTouchOutside == false) {
            return;
        }
        WindowController.Get.HideActiveWindow();
    }
}
