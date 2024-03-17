using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightPos : AttackStyle
{


    public override bool IsInRange(Tile start, List<CharacterController> enemies, out List<CharacterController> targets)
    {
        List<Tile> neighbors = FindEightDir(start, GameManager.Instance().GetMap);
        targets = new List<CharacterController>();
        foreach (var enemy in enemies)
        {
            foreach (Tile neighbor in neighbors)
            {
                Debug.Log($"neighbor - {neighbor.X},{neighbor.Y}");
                if (neighbor == enemy._pos)
                {
                    Debug.Log($"Add Target : {enemy._pos.X}.{enemy._pos.Y}");
                    targets.Add(enemy);
                }
            }
        }

        if (targets.Count <= 0)
        {
            return false;
        }

        return true;
    }
    
    private List<Tile> FindEightDir(Tile tile, Tile[,] grid)
    {
        int gridSizeX = grid.GetLength(0);
        int gridSizeY = grid.GetLength(1);
        List<Tile> neighbors = new List<Tile>();

        for (int x = -Range; x <= Range; x++)
        {
            for (int y = -Range; y <= Range; y++)
            {
                if (x == 0 && y == 0)
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
