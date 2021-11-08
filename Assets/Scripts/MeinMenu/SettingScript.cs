using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [SerializeField]
    private InputField nameSpace;

    public void NameEnter()
    {
        playerStatusScript.playerName = nameSpace.text;
    }
}
