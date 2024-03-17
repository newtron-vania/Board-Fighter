using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : AttackStyle
{
    public override bool IsInRange(Tile start, List<CharacterController> enemies,
        out List<CharacterController> targets)
    {
        targets = new List<CharacterController>();
        foreach (var enemy in enemies)
        {
            if (IsTarget(start, enemy))
            {
                targets.Add(enemy);
            }
        }

        if (targets.Count <= 0)
        {
            return false;
        }

        return true;
    }

    private bool IsTarget(Tile start, CharacterController target)
    {
        if (start - target._pos <= Range)
        {
            return true;
        }

        return false;
    }
    
}
