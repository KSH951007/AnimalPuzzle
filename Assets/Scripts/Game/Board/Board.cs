using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    private Cell[,] cells;
    private int width;
    private int height;


    private void Awake()
    {
        //������ ���̽��� ������ �������������� �о�� �������� ���� ä���

    }
    private void CreateCells()
    {
        cells = new Cell[width, height];

        
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
