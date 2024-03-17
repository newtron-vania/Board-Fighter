using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _anime;
    
    public void Init()
    {
        _anime = GetComponent<Animator>();
    }

    public void Move()
    {
        _anime.Play("walk");
    }

    public void Attack()
    {
        _anime.Play("attack");
    }

    public void Idle()
    {
        _anime.Play("idle");
    }
    
}
