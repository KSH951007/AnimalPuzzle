using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ButtonInfo
{

}
public class MessageBoxUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentTMP;
    [SerializeField] private GameObject parent;
    [SerializeField] private AssetReference buttonPrefab;


    public void SetupMessageBox(string content)
    {
        contentTMP.text = content;

    }

}
