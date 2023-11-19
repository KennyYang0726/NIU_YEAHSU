using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SetUIModeController : MonoBehaviour
{
    public void OnClick()
    {
        var uiModeStr = GetUIModeText();

        if (!(Enum.TryParse(uiModeStr, out UIMode mode) && Enum.IsDefined(typeof(UIMode), mode)))
        {
            Debug.LogError("Setting the ui mode is failed.");
            return;
        }

        if (!MoverioUI.SetUIMode(mode))
        {
            Debug.LogError("Setting the ui mode is failed.");
        }
    }

    private string GetUIModeText()
    {
        var valueDropdownObject = GameObject.Find("SetUIModeValue");
        var uiModeValue = valueDropdownObject.GetComponent<Dropdown>();
        return uiModeValue.options[uiModeValue.value].text;
    }
}
