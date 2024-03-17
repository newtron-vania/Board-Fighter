using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//좌표 저장 및 경로탐색용 Node 클래스
[System.Serializable]
public class Tile : ICloneable
{
    [SerializeField]
    private int x = 0;
    [SerializeField]
    private int y = 0;
    
    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }
    //장애물 확인
    public bool hasObstacle;
    //백트래킹용 부모 타일 저장
    public Tile parent;

    //현재 비용
    public int gCost = 0;
    //휴리스틱 함수 비용
    public int hCost = 0;

    public int fCost
    {
        get { return gCost + hCost; }
    }
    public Tile(int x, int y, bool hasObstacle = false)
    {
        this.x = x;
        this.y = y;
        this.hasObstacle = hasObstacle;
    }
    //타일 간 거리 반환
    public int getDistance(Tile a, Tile b)
    {
        return a - b;
    }
    //타일 간 - 시에도 거리를 반환
    public static int operator -(Tile a, Tile b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    //두 타일의 x,y가 같은 좌표인지 확인
    public static bool operator ==(Tile a, Tile b)
    {
        if (a.x == b.x && a.y == b.y)
        {
            return true;
        }

        return false;
    }
    //두 타일의 x,y가 같은 좌표가 아닌지 확인
    public static bool operator !=(Tile a, Tile b)
    {
        if (a.x != b.x || a.y != b.y)
        {
            return true;
        }

        return false;
    }
    //Clone 함수
    public object Clone()
    {
        return new Tile(x, y, hasObstacle);
    }
}
