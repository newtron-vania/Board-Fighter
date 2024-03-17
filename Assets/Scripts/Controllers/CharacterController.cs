using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Tile _pos;                   //좌표 저장
    /*
     * 전략 패턴 적용
     * MoveType - 이동 방식
     * AttackType - 공격 방식 
     * AttackStyle - 타겟 탐색 방식
     * 이동, 공격, 타겟 방식의 조합에 따라 여러 다른 캐릭터를 만들어낼 수 있다.
     * 필요에 따라 동적으로 조합을 변경할 수 있다.
    */
    private MoveType _moveType;         
    private AttackType _attackType;     
    private AttackStyle _attackStyle;   

    private AnimationController _animator;

    // 진영 확인용 변수
    public Define.WorldObject _worldObject = Define.WorldObject.Unknown;

    private void Start()
    {
        _moveType = GetComponent<MoveType>();
        _attackType = GetComponent<AttackType>();
        _attackType.Damage = GetComponent<Stat>().Damage;
        _attackStyle = GetComponent<AttackStyle>();
        _animator = gameObject.GetOrAddComponent<AnimationController>();
        _animator.Init();
    }

    /*
     * 캐릭터 동작 함수
     * 1. 범위 내 타겟 확인
     * 2. 공격 가능 타겟이 존재할 시 공격 실시
     * 3. 공격 가능 타겟이 존재하지 않을 시 이동
     */
    public void Action(HashSet<GameObject> enemies)
    {
        List<CharacterController> enemiesList = new List<CharacterController>();
        foreach (var mEnemyCharacter in enemies)
        {
            enemiesList.Add(mEnemyCharacter.GetComponent<CharacterController>());
        }
        if(FindInRangedTarget(enemiesList, out List<CharacterController> targets))
        {
            Attack(targets);
        }
        else
        {
            Move(enemiesList);
        }
        
    }

    /* 캐릭터 이동 함수
     * 이동 가능한 좌표가 있을 시 이동 실시
     * 
     */
    public bool Move(List<CharacterController> targets)
    {
        Debug.Log($"{this.transform.name} Move!");
        if (_moveType.Move(_pos, out Tile next, targets))
        {
            _animator.Move();
            Vector3 dir = new Vector3(next.Y - _pos.Y, -(next.X - _pos.X), 0);
            Vector2 nextVec = this.transform.position + dir;
            Vector3 scale = this.transform.localScale;
            //캐릭터 이동위치에 따른 시선 변경
            if (dir.x < 0)
            {
                scale.x = Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            }
            else if (dir.x > 0)
            {
                scale.x = -Mathf.Abs(scale.x);
                this.transform.localScale = scale;
            }
            //타일맵 갱신
            GameManager.Instance().GetMap[_pos.X, _pos.Y].hasObstacle = false;
            GameManager.Instance().GetMap[next.X, next.Y].hasObstacle = true;
            _pos = next;
            Debug.Log($"{this.transform.name} next is ({_pos.X} , {_pos.Y})!");
            Debug.Log($"{this.transform.name} next Vector is {nextVec}");

            StartCoroutine(Moving(this.transform.position, nextVec, _lerpTime));
        }
        
        return false;
    }

    private float _lerpTime = 0.5f;
    IEnumerator Moving(Vector3 current, Vector3 next, float time)
    {
        float elapsedTime = 0.0f;
        this.transform.position = current;
        while (elapsedTime < time)
        {
            elapsedTime += Time.fixedDeltaTime;
            this.transform.position = Vector3.Lerp(current, next, elapsedTime / time);
            yield return new WaitForFixedUpdate();
        }

        this.transform.position = next;
        _animator.Idle();
        yield return null;
    }

    public bool Attack(List<CharacterController> targets)
    {
        Debug.Log($"{this.transform.name} Attack!");
        _animator.Attack();
        _attackType.Attack(targets);
        return false;
    }

    public bool FindInRangedTarget(List<CharacterController> enemies, out List<CharacterController> targets)
    {
        Debug.Log("Detectiong! ");
        if (_attackStyle.IsInRange(_pos, enemies, out targets))
        {
            return true;
        }
        return false;
    }
}
