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
        //���� ���� 2ĭ���̸� ��ǥ ���� ���� ����
        //���� 3ĭ,����3ĭ�̸� �ֺ� 5x5 ����
        //���� 5 or ���� 5ĭ ������ �������� ��ü ��Ʈ��
        //���η� 5ĭ and ���� 5ĭ�̸� ��ü�� ��Ʈ��

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //����� ���϶��� Ŭ������ ��ȣ�ۿ�
        //�⺻ ���� ���� Ŭ������ ��ȣ�ۿ� x
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //ó�� ���� ���� ����
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //���� ��ġ�� ����
        //ó����ġ�� 1ĭ ���� �� ���̳��� ��ȿ
        //����Ⱥ��̶�� �ٲ��ڸ����� ��ȣ�ۿ�

    }
}
