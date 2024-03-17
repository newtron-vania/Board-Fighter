using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackType : MonoBehaviour
{
    protected int _damage = 0;

    public int Damage
    {
        set { _damage = value; }
    }
    public abstract bool Attack(List<CharacterController> targets);

    protected virtual void GiveDamage(CharacterController target)
    {
        target.GetComponent<Stat>().Hp -= _damage;
    }

}
