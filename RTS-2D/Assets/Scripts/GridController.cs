using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
public class GridController : MonoBehaviour
{
    [SerializeField] public Vector2Int gridSize;
    [SerializeField] public float cellRadius = 0.5f;

    //SerializeField] public class Pathfinding PathFind;
    public Pathfinding PathFind;

    static public float[,] tilesmap;
    public Cell grid;
    private void InitPathfinding(){
        // create the tiles map
        tilesmap = new float[gridSize.x, gridSize.y];
        // set values here....
        // every float in the array represent the cost of passing the tile at that position.
        // use 0.0f for blocking tiles.

        System.Random _rand = new System.Random();
        for (int i = 0; i < gridSize.x; ++i)
        {
            for (int j = 0; j < gridSize.y; ++j)
            {
                tilesmap[i, j] = (float)_rand.NextDouble();
            }
        }

        // create a grid
        grid = new Cell(tilesmap);
        // for Manhattan distance
        //List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan);
    }

    private void Start() {
        InitPathfinding();
        
    }

    private void Update() {
              
    }
}
