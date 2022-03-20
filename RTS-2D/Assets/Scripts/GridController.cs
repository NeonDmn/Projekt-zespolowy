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
    private void InitPathfinding()
    {
        // create the tiles map
        tilesmap = new float[gridSize.x, gridSize.y];
        // set values here....
        // every float in the array represent the cost of passing the tile at that position.
        // use 0.0f for blocking tiles.


        ///     0.0f = Unwalkable tile.
        ///     1.0f = Normal tile.
        ///     > 1.0f = costy tile.
        ///     < 1.0f = cheap tile.

        GameObject it = GameObject.Find("Grid");
        GameObject second = it.gameObject.transform.GetChild(1).gameObject;
        
        Tilemap test = second.GetComponent<Tilemap>();


        for (int i = 0; i < gridSize.x; ++i)
        {
            for (int j = 0; j < gridSize.y; ++j)
            {
                tilesmap[i, j] = 1.0f;
            }
        }

        foreach (var position in test.cellBounds.allPositionsWithin)
        {
            if (!test.HasTile(position))
            {

            }else{
                Debug.Log(position);
                tilesmap[(int)position.x,(int)position.y] =0.0f;
            }

            // Tile is not empty; do stuff
        }

        //Tilemap tilemap = GetComponent<Tilemap>();
        //
        //BoundsInt bounds = tilemap.cellBounds;
        //TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        //
        //for (int x = 0; x < bounds.size.x; x++)
        //{
        //    for (int y = 0; y < bounds.size.y; y++)
        //    {
        //        TileBase tile = allTiles[x + y * bounds.size.x];
        //        if (tile != null)
        //        {
        //            Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
        //        }
        //        else
        //        {
        //            Debug.Log("x:" + x + " y:" + y + " tile: (null)");
        //        }
        //    }
        //}
        //

        // create a grid
        grid = new Cell(tilesmap);
        
        // for Manhattan distance
        //List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan);
    }

    private void Start()
    {
        InitPathfinding();

    }

    private void Update()
    {

    }
}
