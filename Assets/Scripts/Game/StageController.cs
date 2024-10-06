using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private Player player;

    private StageData stageData;
    [SerializeField] private BlockSpawner spawner;
    [SerializeField] private BlockController blockController;
    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();




    }
    private void Start()
    {

        stageData = (StageData)GameManager.Instance.currentStageData.Clone();


        blockController.Init(stageData);
    }


}
