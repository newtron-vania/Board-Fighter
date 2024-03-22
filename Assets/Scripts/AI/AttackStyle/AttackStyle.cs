using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStyle : MonoBehaviour
{
    [SerializeField]
    private int _range = 0;
    
    public int Range
    {
        get { return _range; }
    }
    public abstract bool IsInRange(Tile start, List<CharacterController> enemies, out List<CharacterController> targets);

}
