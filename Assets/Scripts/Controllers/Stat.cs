using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] 
    private int _maxHp;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _damage;

    public int MaxHp
    {
        get { return _maxHp; }
    }
    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            isDead();
        }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public bool isDead()
    {
        if (_hp <= 0)
        {
            GameManager.Instance().Despawn(this.gameObject);
            return true;
        }
        
        return false;
    }
}
