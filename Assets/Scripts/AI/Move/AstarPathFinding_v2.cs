using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

// Astar 알고리즘 을 통한 최단거리 탐색 클래스
public class AstarPathFinding : PathFinding
{
    // 배열 최대 크기 확인용 변수
    private int gridSizeX = 0, gridSizeY = 0;
    
    public List<Tile> GetNextPath(Tile start, Tile target, bool ignoreObstacle = false)
    {
        Tile[,] initialGrid = InitializeGrid();
        initialGrid[target.X, target.Y].hasObstacle = false;

        List<Tile> path = FindPath(start, target, initialGrid, ignoreObstacle);

        return path;
    }

    // 경로 탐색용 2차원 타일 배열 생성
    public Tile[,] InitializeGrid()
    {
        Tile[,] original = GameManager.Instance().GetMap;
        gridSizeX = original.GetLength(0);
        gridSizeY = original.GetLength(1);
        Tile[,] initialGrid = new Tile[gridSizeX, gridSizeY];

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                // Tile DeepCopy
                initialGrid[i, j] = original[i, j].Clone() as Tile;
            }
        }

        return initialGrid;
    }

    /*
     * Astar Alrogithm
     * g = 해당 경로까지의 최단 이동 횟수
     * h = 목표 좌표와 현재 좌표까지의 남은 이동거리
     * f = g + h
    */
    private List<Tile> FindPath(Tile startNode, Tile targetNode, Tile[,] grid, bool ignoreObstacle = false)
    {
        List<Tile> openSet = new List<Tile>();
        List<Tile> closedSet = new List<Tile>();
        
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentTile.fCost ||
                    (openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost))
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);


            if (currentTile == targetNode)
            {
                return RetracePath(startNode, currentTile);
            }

            foreach (Tile neighbor in GetNeighbors(currentTile, grid))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }
                // 장애물 무시여부에 따라 이동가능여부 확인
                if (neighbor.hasObstacle)
                {
                    if(!ignoreObstacle)
                        continue;
                }

                int newCostToNeighbor = currentTile.gCost + (neighbor - currentTile);
                
                
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = targetNode - neighbor;
                    neighbor.parent = currentTile;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }


        return null;
    }
    
    // 최단경로 백트래킹
    private List<Tile> RetracePath(Tile startNode, Tile endNode)
    {
        List<Tile> path = new List<Tile>();
        Tile currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    // 4 방향 배열 내 좌표 확인 및 반환
    private List<Tile> GetNeighbors(Tile tile, Tile[,] grid)
    {
        List<Tile> neighbors = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0 && y == 0 )|| (Mathf.Abs(x) + Mathf.Abs(y) >= 2))
                    continue;

                int checkX = tile.X + x;
                int checkY = tile.Y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }
}
