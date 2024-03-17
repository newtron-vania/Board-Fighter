using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single : AttackType
{
    public override bool Attack(List<CharacterController> targets)
    {
        if (targets == null || targets.Count <= 0)
        {
            return false;
        }
        
        GiveDamage(targets[0]);

        return true;
    }
}
