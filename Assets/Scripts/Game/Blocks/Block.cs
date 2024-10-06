using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;
using UnityEngine.EventSystems;



public abstract class Block : MonoBehaviour
{

    protected Vector2Int blockPos;
    protected IExplodable explodable;

    [SerializeField] private BlockID blockID;


    public BlockID GetBlockID()
    {
        return blockID;
    }



}
