using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetKeyLockController : MonoBehaviour
{
    private Text keyLockValueText;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("GetKeyLockValue");
        keyLockValueText = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {

        if (keyLockValueText == null)
        {
            return;
        }

        if (!MoverioUI.IsActive())
        {
            return;
        }

        var keyLockKeyStr = GetKeyLockText();
        if (!(Enum.TryParse(keyLockKeyStr, out KeyLock mode) && Enum.IsDefined(typeof(KeyLock), mode)))
        {
            Debug.LogError("Getting the key lock is failed.");
            keyLockValueText.text = "-";
            return;
        }

        try
        {
            var getValue = MoverioUI.GetKeyLock();
            keyLockValueText.text = getValue.ToString();
        }
        catch (IOException e)
        {
            keyLockValueText.text = "-";
            Debug.LogError("Getting the key lock is failed. Message = " + e.Message);
        }
    }

    private string GetKeyLockText()
    {
        var valueDropdownObject = GameObject.Find("SetKeyLockValue");
        var keyLockValue = valueDropdownObject.GetComponent<Dropdown>();
        return keyLockValue.options[keyLockValue.value].text;
    }
}
