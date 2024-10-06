using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockSpanwpPrefab
{
    public BlockID blockID;
    public GameObject blockPrefab;
}
public class BlockSpawner : MonoBehaviour
{

    private Dictionary<BlockID, Stack<Block>> blockDic;

    [SerializeField]
    private BlockSpanwpPrefab[] blockPrefabs;
    // Start is called before the first frame update
    private void Awake()
    {
        blockDic = new Dictionary<BlockID, Stack<Block>>();

    }
    void Start()
    {
        Init();



    }
    public void Init()
    {
        StageData stageData = GameManager.Instance.currentStageData;
        int poolCount = 50;
        for (int i = 0; i < stageData.BlockList.Count; i++)
        {
            BlockID blockType = stageData.BlockList[i];

            Stack<Block> blocks = new Stack<Block>(poolCount);
            blockDic.Add(blockType, blocks);
            for (int j = 0; j < poolCount; j++)
            {
                GameObject blockObj = Instantiate(blockPrefabs[(int)blockType].blockPrefab, this.transform);
                blockObj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                blockObj.SetActive(false);
                blocks.Push(blockObj.GetComponent<Block>());
            }

        }
    }

    public Block GetBlock(BlockID blockType, Transform parent)
    {
        if (!blockDic.ContainsKey(blockType))
            return null;

        Block block = blockDic[blockType].Pop();

        block.transform.SetParent(parent);
        block.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        block.gameObject.SetActive(true);

        return block;

    }

}
