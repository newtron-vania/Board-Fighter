using System.Collections;
using System.Collections.Generic;
using AI;
using Unity.VisualScripting;
using UnityEngine;

// 이동 방식 정의 추상 인터페이스
public abstract class MoveType : MonoBehaviour
{
    // 사용할 최단거리 알고리즘 선택
    public PathFinding PathFinding;

    private void Start()
    {
        PathFinding = new AstarPathFinding();
    }
    // 다음 이동 좌표 반환 및 이동 성공 여부 확인
    public abstract bool Move(Tile start, out Tile next, List<CharacterController> targets);
    
    
}
