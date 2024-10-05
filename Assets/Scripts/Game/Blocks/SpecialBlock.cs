using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpecialBlock : Block
{
    protected override void DestroyBlock()
    {
        throw new NotImplementedException();
    }

    protected override bool FindMatches()
    {
        return true;
    }
}

