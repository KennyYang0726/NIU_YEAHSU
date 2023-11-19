using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetControllerLedModeController : MonoBehaviour
{
    private Text controllerLedModeValueText;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("GetControllerLedModeValue");
        controllerLedModeValueText = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {

        if (controllerLedModeValueText == null)
        {
            return;
        }

        if (!MoverioUI.IsActive())
        {
            return;
        }

        var controllerLedModeKeyStr = GetControllerLedModeText();
        if (!(Enum.TryParse(controllerLedModeKeyStr, out ControllerLedMode mode) && Enum.IsDefined(typeof(ControllerLedMode), mode)))
        {
            Debug.LogError("Getting the controller led mode is failed.");
            controllerLedModeValueText.text = "-";
            return;
        }

        try
        {
            var getValue = MoverioUI.GetControllerLedMode();
            controllerLedModeValueText.text = getValue.ToString();
        }
        catch (IOException e)
        {
            controllerLedModeValueText.text = "-";
            Debug.LogError("Getting the controller led mode is failed. Message = " + e.Message);
        }
    }

    private string GetControllerLedModeText()
    {
        var valueDropdownObject = GameObject.Find("SetControllerLedModeValue");
        var controllerLedModeValue = valueDropdownObject.GetComponent<Dropdown>();
        return controllerLedModeValue.options[controllerLedModeValue.value].text;
    }
}
