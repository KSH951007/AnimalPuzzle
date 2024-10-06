using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    private readonly string stageTitleForm = @"Stage {0}";
    [SerializeField] private TextMeshProUGUI stageTitleTMP;
    [SerializeField] private RectTransform goalListParentTr;
    [SerializeField] private RectTransform itemListParentTr;
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);

    }
    public void SetStageInfo(StageData stageData)
    {
        stageTitleTMP.text = string.Format(stageTitleForm, stageData.StageLevel);

    }

    public void PressStartButton()
    {
        StartCoroutine(SceneLoader.Instance.NextSceneLoadAsync("StageScene"));
    }
}
