using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    private readonly string stageTitleForm = @"Stage {0}";
    [SerializeField] private TextMeshProUGUI stageTitleTMP;


    public void SetStageInfo(int stageIndex)
    {
        stageTitleTMP.text = string.Format(stageTitleForm, stageIndex);

    }

    public void PressStartButton()
    {

    }
}
