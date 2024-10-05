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
        //데이터 베이스에 접근해 스테이지정보를 읽어와 셀정보와 블럭을 채운다

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
