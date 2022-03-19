using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    public Cell[,] grid {get; private set;}
    public Vector2Int gridSize {get; private set;}
    public  float cellRadius {get; private set;}
    public float cellDiameter{get; private set;}

    public FlowField(float _cellRadius, Vector2Int _gridSize){
        cellRadius = _cellRadius;
        cellDiameter = cellRadius*2;
        gridSize = _gridSize;
    }
    public void CreateGrid(){
        grid = new Cell[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 worldPos = new Vector2(cellDiameter*i+cellRadius,cellDiameter*j+cellRadius);
                grid[i,j]= new Cell(worldPos,new Vector2Int(i,j));

            }
        }
    }


    public void CreateCostField()
    {
        //Vector2 cellHalfExtents = Vector2.one * cellRadius;
        int terrainMask = LayerMask.GetMask("Impassable", "RoughTerrain");
        foreach (Cell curCell in grid)
        {
            Debug.Log(curCell.gridIndex);
            Collider2D obstacles = Physics2D.OverlapBox(curCell.worldPos,new Vector2(100,100) ,0);
            bool hasIncreasedCost = false;
                Debug.Log(obstacles);
                if (obstacles.gameObject.layer == 8)
                {
                    curCell.IncreaseCost(255);
                    continue;
                }
                else if (!hasIncreasedCost && obstacles.gameObject.layer == 9)
                {
                    curCell.IncreaseCost(3);
                    hasIncreasedCost = true;
                }
        }
    }
}
