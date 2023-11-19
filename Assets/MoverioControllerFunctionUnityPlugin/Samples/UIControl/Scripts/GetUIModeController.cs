using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetUIModeController : MonoBehaviour
{
    private Text uiModeValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("GetUIModeValue");
        uiModeValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {

        if (uiModeValue == null)
        {
            return;
        }

        if (!MoverioUI.IsActive())
        {
            return;
        }

        var uiModeKeyStr = GetUIModeText();
        if (!(Enum.TryParse(uiModeKeyStr, out UIMode mode) && Enum.IsDefined(typeof(UIMode), mode)))
        {
            Debug.LogError("Getting the ui mode is failed.");
            uiModeValue.text = "-";
            return;
        }

        try
        {
            var getValue = MoverioUI.GetUIMode();
            uiModeValue.text = getValue.ToString();
        }
        catch (IOException e)
        {
            uiModeValue.text = "-";
            Debug.LogError("Getting the ui mode is failed. Message = " + e.Message);
        }
    }

    private string GetUIModeText()
    {
        var valueDropdownObject = GameObject.Find("SetUIModeValue");
        var uiModeValue = valueDropdownObject.GetComponent<Dropdown>();
        return uiModeValue.options[uiModeValue.value].text;
    }
}
