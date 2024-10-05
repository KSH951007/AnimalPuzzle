using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    Block[,] blocksLayers;
    private int blockColumnsCount;
    private int blockRowCount;

    private bool isProgress;
    private GridLayoutGroup blockLayoutGroup;
    private readonly Vector2Int[] directon = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };

    private Vector2Int? beginSelectBlock;
    private void Awake()
    {
        blockLayoutGroup = GetComponent<GridLayoutGroup>();
        isProgress = false;


        blockColumnsCount = blockLayoutGroup.constraintCount;
        blockRowCount = this.transform.childCount / blockColumnsCount;
        blocksLayers = new Block[blockRowCount, blockColumnsCount];
        for (int i = 0; i < blockRowCount; i++)
        {
            for (int j = 0; j < blockColumnsCount; j++)
            {
                Transform blockTr = this.transform.GetChild(i * blockColumnsCount + j);

                blocksLayers[i, j] = blockTr.GetComponent<Block>();
                blocksLayers[i, j].onBeginDrag += OnBeginDrag;
                blocksLayers[i, j].onDrop += SwapBlock;
                blocksLayers[i, j].Setup(new Vector2Int(i, j));
            }
        }
    }



    public List<Vector2Int> FindMaches(int x, int y)
    {

        List<Vector2Int> matchBlocks = new List<Vector2Int>();
        bool[,] isVisited = new bool[blockRowCount, blockColumnsCount];

        Vector2Int matchCount = new Vector2Int(0, 0);
        int columnCount = 0;
        int rowCount = 0;

        BlockType blockType = blocksLayers[x, y].blockType;



        DFS(x, y, blockType, isVisited, matchBlocks, ref matchCount);


        if (TryCreateSpecialBlock(matchCount, out Block specialBlock))
        {

        }


        return matchBlocks;
    }
    public void DFS(int x, int y, BlockType blockType, bool[,] isVisited, List<Vector2Int> matchBlocks, ref Vector2Int matchCount)
    {
        if (isVisited[x, y] == true)
            return;


        isVisited[x, y] = true;

        matchBlocks.Add(new Vector2Int(x, y));

        for (int i = 0; i < directon.Length; i++)
        {
            int newDirX = x + directon[i].x;
            int newDirY = y + directon[i].y;

            matchCount.x += directon[i].x;
            matchCount.y += directon[i].y;
            DFS(newDirX, newDirY, blockType, isVisited, matchBlocks, ref matchCount);
        }
    }
    public bool TryCreateSpecialBlock(in Vector2Int matchCount, out Block specialBlock)
    {
        //가로 새로 2칸씩이면 목표 블럭을 향해 터트리는 블럭생성

        //가로 3칸,세로3칸이면 주변 5x5 터트리는 블럭생성
        //가로 5 or 세로 5칸 스왑한 같은블럭을 전체 터트리는 블럭생성
        //가로로 5칸 and 세로 5칸이면 전체블럭 터트리는 블럭생성

        specialBlock = null;

        return true;
    }
    public void DestroyBlocks(List<Vector2Int> blocks)
    {


    }


    public void OnBeginDrag(Vector2Int pos)
    {
        //이동이 가능한 블럭인지 확인
        if (blocksLayers[pos.x, pos.y].isMoveable == false)
            return;

        beginSelectBlock = pos;


        Debug.Log(pos);
    }

    public void SwapBlock(Vector2Int pos)
    {
        if (beginSelectBlock.HasValue == false)
            return;

        if (IsSwapBlocks(beginSelectBlock.Value, pos) == false)
            return;


      

        FindMaches(pos.x, pos.y);



        beginSelectBlock = null;

        Debug.Log(pos);
    }
    public bool IsSwapBlocks(Vector2Int firstBlock, Vector2Int secondBlock)
    {

        if (Vector2Int.Distance(firstBlock, secondBlock) == 1)
            return true;

        return false;
    }
}
