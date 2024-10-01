using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum BlockType
{
    Rabbit,

}
public abstract class Block : MonoBehaviour
{
    protected Sprite blockIcon;
    protected int blockSizeX = 1;
    protected int blockSizeY = 1;

    public BlockType blockType { get; protected set; }

    //public abstract void 

    protected abstract bool FindMatches();

}
