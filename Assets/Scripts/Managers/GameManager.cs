using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Managers<GameManager>
{

    public int selectStageLevel { get; private set; }
    private void Awake()
    {
        if (!base.Init())
            return;


    }


    public void SetStageLevel(int stageLevel)
    {
        this.selectStageLevel = stageLevel;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
