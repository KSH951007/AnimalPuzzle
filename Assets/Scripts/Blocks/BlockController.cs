using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockController : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IEndDragHandler
{
    Block[,] blocksLayers;
    private int blockColumnsCount;
    private int blockRowCount;


    private readonly Vector2Int[] directon = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 1) };

    private void Awake()
    {
        blocksLayers = new Block[blockRowCount, blockColumnsCount];
    }



    public List<Vector2Int> FindMaches(int x, int y)
    {

        List<Vector2Int> matchBlocks = new List<Vector2Int>();
        bool[,] isVisited = new bool[blockRowCount, blockColumnsCount];


        BlockType blockType = blocksLayers[x, y].blockType;
        DFS(x, y, blockType, isVisited, matchBlocks);

        return matchBlocks;
    }
    public void DFS(int x, int y, BlockType blockType, bool[,] isVisited, List<Vector2Int> matchBlocks)
    {
        isVisited[x, y] = true;
        matchBlocks.Add(new Vector2Int(x, y));

        for (int i = 0; i < directon.Length; i++)
        {
            int newDirX = x + directon[i].x;
            int newDirY = y + directon[i].y;

            DFS(newDirX, newDirY, blockType, isVisited, matchBlocks);
        }
        

    }
    public void DestroyBlocks(List<Vector2Int> blocks)
    {
        //가로 새로 2칸씩이면 목표 블럭을 향해 터짐
        //가로 3칸,세로3칸이면 주변 5x5 터짐
        //가로 5 or 세로 5칸 스왑한 같은블럭을 전체 터트림
        //가로로 5칸 and 세로 5칸이면 전체블럭 터트림

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //스폐셜 블럭일때는 클릭으로 상호작용
        //기본 동물 블럭은 클릭으로 상호작용 x
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //처음 잡은 블럭을 선택
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //놓는 위치를 선택
        //처음위치와 1칸 보다 더 차이나면 무효
        //스폐셜블럭이라면 바뀐자리에서 상호작용

    }
}
