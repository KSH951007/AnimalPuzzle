using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public enum BlockDirection
{
    Left,
    Right,
    Top,
    Bottom,
    End
}
public class BlockController : MonoBehaviour
{
    private StageData stageData;
    [SerializeField] private BlockSpawner spawner;
    [SerializeField] private GameObject cellPrefab;
    private Cell[,] cells;
    private Vector2Int[] directionArray;
    private void Awake()
    {
        directionArray = new Vector2Int[(int)BlockDirection.End];
        directionArray[(int)BlockDirection.Left] = new Vector2Int(-1, 0);
        directionArray[(int)BlockDirection.Right] = new Vector2Int(1, 0);
        directionArray[(int)BlockDirection.Top] = new Vector2Int(0, -1);
        directionArray[(int)BlockDirection.Bottom] = new Vector2Int(0, 1);
    }
    public void Init(StageData stageData)
    {
        CreateBoard(stageData);
        SetupBlock(stageData);
    }
    public void CreateBoard(StageData stageData)
    {
        if (stageData == null)
            return;

        float centerRow = stageData.IsPresenceCells.GetLength(1) / 2f;
        float centerColumn = stageData.IsPresenceCells.GetLength(0) / 2f;
        cells = new Cell[stageData.BoardRowCount, stageData.BoardHeightCount];

        for (int i = 0; i < stageData.IsPresenceCells.GetLength(1); i++)
        {
            for (int j = 0; j < stageData.IsPresenceCells.GetLength(0); j++)
            {
                float xPos = 0 - centerColumn + j;
                float yPos = 0 - centerRow + i;

                if (stageData.IsPresenceCells[i, j] == true)
                {
                    GameObject cellObj = Instantiate(cellPrefab, transform);
                    cellObj.transform.position = new Vector2(xPos, yPos);
                    cells[i, j] = cellObj.GetComponent<Cell>();
                }
                else
                {
                    cells[i, j] = null;
                }


            }

        }
    }
    public void SetupBlock(StageData stageData)
    {

        int blockCount = stageData.BlockList.Count;
        for (int i = 0; i < cells.GetLength(1); i++)
        {
            for (int j = 0; j < cells.GetLength(0); j++)
            {
                int randIndex;
                BlockID blockType;
                do
                {
                    randIndex = Random.Range(0, blockCount);
                    blockType = stageData.BlockList[randIndex];

                } while (!IsSetupMatch(blockType, i, j));

                if (cells[i, j] != null)
                {
                    Block block = spawner.GetBlock(blockType, cells[i, j].transform);
                    cells[i, j].SetBlock(block);
                }
            }
        }
    }

    public bool IsSetupMatch(BlockID blockType, int x, int y)
    {
        int rowMatchCount = 1;
        int colMatchCount = 1;



        for (int i = 0; i < directionArray.Length; i++)
        {


            for (int j = 1; j < 3; j++)
            {
                int posX = x + directionArray[i].x * j;
                int posY = y + directionArray[i].y * j;
                if (posX <= 0 || posX >= cells.GetLength(1))
                    break;
                if (posY <= 0 || posY >= cells.GetLength(0))
                    break;
                if (cells[posX, posY] == null || cells[posX, posY].isEmpty == true)
                    break;


                if (cells[posX, posY].block.GetBlockID() == blockType)
                {

                    if (posX + x <= -1 || posX + x >= 1)
                    {
                        rowMatchCount++;
                    }
                    else if (posY + y <= -1 || posY + y >= 1)
                    {
                        colMatchCount++;
                    }

                }

            }
        }

        if (rowMatchCount >= 3 || colMatchCount >= 3)
            return false;
        else
            return true;
    }

    private void Update()
    {
     //   Touchscreen.current.primaryTouch.position
    }

}
