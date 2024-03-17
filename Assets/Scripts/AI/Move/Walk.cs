using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

//장애물이 있는 경로는 이동할 수 없는 MoveType 구현 클래스
public class Walk : MoveType
{

    public override bool Move(Tile start, out Tile next, List<CharacterController> targets)
    {
        next = null;
        if (targets.Count <= 0)
        {
            return false;
        }
        next = FindPath(start, targets);
        if (next is null)
        {
            return false;
        }
        return true;
    }
    
    // 적대 유닛들 중 최단거리(경로 수)인 에서 첫 번째 위치를 반환
    private Tile FindPath(Tile start, List<CharacterController> targets)
    {
        List<Tile> shortestPath = new List<Tile>();
        foreach (var enemy in targets)
        {
            List<Tile> path = PathFinding.GetNextPath(start, enemy._pos);
            if (shortestPath.Count <= 0 || path.Count < shortestPath.Count)
            {
                shortestPath = path;
            }
        }

        if (shortestPath == null)
        {
            return null;
        }
        
        return shortestPath[0];
    }
}
