using MoverioControllerFunctionUnityPlugin;
using MoverioControllerFunctionUnityPlugin.Type;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetKeyLockController : MonoBehaviour
{
    public void OnClick()
    {
        var keyLockStr = GetKeyLockText();

        if (!(Enum.TryParse(keyLockStr, out KeyLock mode) && Enum.IsDefined(typeof(KeyLock), mode)))
        {
            Debug.LogError("Setting the key lock is failed.");
            return;
        }

        /*
         * KeyLockを有効にすると、物理キー及びUI操作ができなくなる
         * KeyLockを有効にした時は5秒経過後に無効設定する
         */
        if (MoverioUI.SetKeyLock(mode))
        {
            if(mode == KeyLock.KEY_LOCK_ENABLE)
            {
                StartCoroutine(SetDisableCoroutine());
            }
        }
        else
        {
            Debug.LogError("Setting the key lock is failed.");
        }
    }

    private string GetKeyLockText()
    {
        var valueDropdownObject = GameObject.Find("SetKeyLockValue");
        var keyLockValue = valueDropdownObject.GetComponent<Dropdown>();
        return keyLockValue.options[keyLockValue.value].text;
    }

    // KeyLock有効時に実行する、無効設定用コルーチン
    private IEnumerator SetDisableCoroutine()
    {
        // 5秒間待つ
        yield return new WaitForSeconds(5);

        // KeyLockを無効化
        MoverioUI.SetKeyLock(KeyLock.KEY_LOCK_DISABLE);
    }
}
