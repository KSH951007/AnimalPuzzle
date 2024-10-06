using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Managers<GameManager>
{
    public StageData currentStageData
    {
        get; private set;
    }
    public int currentStageLevel { get; private set; }

    public event Action<StageData> onStageInfoChanged;
    private void Awake()
    {
        if (!base.Init())
            return;

        SceneManager.sceneLoaded += SetupSceneChagne;
    }

    public void SetupSceneChagne(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (DataManager.instance.FindData(ResponseDataType.Stage))
        {
            StageResponse stageResponse = (StageResponse)DataManager.instance.GetData(ResponseDataType.Stage);
            LoadStageData(stageResponse);
        }

    }


    public void LoadStageData(StageResponse stageResponse)
    {
        if (stageResponse != null)
        {

            if (currentStageData == null)
                currentStageData = new StageData();

            currentStageData.StageLevel = stageResponse.StageLevel;
            currentStageData.StepCount = stageResponse.StepCount;
            currentStageData.BoardHeightCount = stageResponse.BoardHeightCount;
            currentStageData.BoardRowCount = stageResponse.BoardRowCount;
            //currentStageData.BlockList = new List<BlockID>();
            currentStageData.BlockList = JsonConvert.DeserializeObject<List<BlockID>>(stageResponse.BlockList);
            // currentStageData.IsPresenceCells = new bool[stageResponse.BoardRowCount, stageResponse.BoardHeightCount];
            currentStageData.IsPresenceCells = JsonConvert.DeserializeObject<bool[,]>(stageResponse.IsPresenceCells);
            onStageInfoChanged(currentStageData);
        }
    }

}

public class StageData : ICloneable
{
    public int StageLevel { get; set; }
    public int StepCount { get; set; }
    public int BoardRowCount { get; set; }
    public int BoardHeightCount { get; set; }

    public bool[,] IsPresenceCells { get; set; }
    public List<BlockID> BlockList { get; set; }

    public object Clone()
    {
        StageData stageData = new StageData();
        stageData.StageLevel = StageLevel;
        stageData.StepCount = StepCount;
        stageData.BoardRowCount = BoardRowCount;
        stageData.BoardHeightCount = BoardHeightCount;
        stageData.IsPresenceCells = new bool[BoardRowCount, BoardHeightCount];
        for (int i = 0; i < IsPresenceCells.GetLength(1); i++)
        {
            for (int j = 0; j < IsPresenceCells.GetLength(0); j++)
            {
                stageData.IsPresenceCells[i, j] = IsPresenceCells[i, j];
            }
        }
        stageData.BlockList = new List<BlockID>();
        for (int i = 0; i < BlockList.Count; i++)
        {
            stageData.BlockList.Add(BlockList[i]);
        }
    


        return stageData;
    }
}
