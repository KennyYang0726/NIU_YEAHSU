using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SetControllerLedModeController : MonoBehaviour
{
    public void OnClick()
    {
        var controllerLedModeStr = GetControllerLedModeText();

        if (!(Enum.TryParse(controllerLedModeStr, out ControllerLedMode mode) && Enum.IsDefined(typeof(ControllerLedMode), mode)))
        {
            Debug.LogError("Setting the controller led mode is failed.");
            return;
        }

        if (!MoverioUI.SetControllerLedMode(mode))
        {
            Debug.LogError("Setting the controller led mode is failed.");
        }
    }

    private string GetControllerLedModeText()
    {
        var valueDropdownObject = GameObject.Find("SetControllerLedModeValue");
        var controllerLedModeValue = valueDropdownObject.GetComponent<Dropdown>();
        return controllerLedModeValue.options[controllerLedModeValue.value].text;
    }
}
