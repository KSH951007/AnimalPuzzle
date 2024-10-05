using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Managers<PoolManager>
{



    private void Awake()
    {
        if (!base.Init())
            return;


    }

}
