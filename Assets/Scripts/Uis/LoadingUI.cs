using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadAmountTMP;
    [SerializeField] private Image loadFillAmount;
    private readonly string progressFormat = @"( {0} / {1} )";
    private void Start()
    {
        SceneLoader.Instance.onPrgressLoad += ShowProgress;
    }
    private void OnDestroy()
    {
        SceneLoader.Instance.onPrgressLoad -= ShowProgress;
    }
    public void ShowProgress(int currentProgress, int totalProgress)
    {
        loadAmountTMP.text = string.Format(progressFormat, currentProgress, totalProgress);
        loadFillAmount.fillAmount = (float)currentProgress / totalProgress;
    }

}
