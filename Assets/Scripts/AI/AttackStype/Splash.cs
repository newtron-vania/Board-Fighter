using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : AttackType
{
    public override bool Attack(List<CharacterController> targets)
    {
        if (targets == null || targets.Count <= 0)
        {
            return false;
        }

        foreach (var target in targets)
        {
            GiveDamage(target);
        }
        return false;
    }
}

