using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalBlock : Block
{
    private void Awake()
    {
        isMoveable = true;
    }
 
    protected override void DestroyBlock()
    {

    }

    protected override bool FindMatches()
    {

        return true;
    }


}
