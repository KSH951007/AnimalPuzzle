using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cell : MonoBehaviour
{

    public Block block { get; private set; }
    public bool isEmpty { get; private set; }

    private void Awake()
    {
        isEmpty = true;
    }
    public void SetBlock(Block block)
    {
        this.block = block;

        isEmpty = block == null ? true : false;
    }




}
