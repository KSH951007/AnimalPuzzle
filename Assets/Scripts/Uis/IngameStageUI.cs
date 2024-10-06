using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameStageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageLevelTMP;
    [SerializeField] private TextMeshProUGUI stepCountTMP;

    private void Start()
    {
        SetupStageUI(GameManager.Instance.currentStageData);
    }


    public void SetupStageUI(StageData stageData)
    {
        if (stageData == null)
            return;

        stageLevelTMP.text = $"Stage {stageData.StageLevel}";
        ChangeStepCountUI(stageData.StepCount);
    }
    public void ChangeStepCountUI(int newStepCount)
    {
        stepCountTMP.text = newStepCount.ToString();
    }
}
