using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockData
{



}
public abstract class Block : MonoBehaviour
{
    private Sprite blockIcon;


    //터졌을때 기능
    public abstract void ExplodeBlock();
}
