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

    static public float[,] tilesmap;
    static public Node[,] tileMapNodes;

    private Node end = null;
    

    private void InitTileMapPathfinding(){

        tileMapNodes = new Node[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                tileMapNodes[i, j] = new Node(i, j);
                tileMapNodes[i, j].isObstacle = false;
                tileMapNodes[i, j].parent = null;
                tileMapNodes[i, j].isVisited = false;
            }
        }
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                if (j > 0)
                {
                    tileMapNodes[i, j].neighBours.Add(tileMapNodes[i, j - 1]);
                }
                if (j < (gridSize.y - 1))
                {
                    tileMapNodes[i, j].neighBours.Add(tileMapNodes[i, j + 1]);
                }
                if (i > 0)
                {
                    tileMapNodes[i, j].neighBours.Add(tileMapNodes[i - 1, j]);
                }
                if (i < (gridSize.x - 1))
                {
                    tileMapNodes[i, j].neighBours.Add(tileMapNodes[i + 1, j]);
                }
            }

        }
        GameObject  test = GameObject.Find("Trees");
        //foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        //{
        //    Debug.Log(g.name);
        //}

        //Debug.Log(test.transform.childCount);
        foreach (Transform child in test.transform){
            Debug.Log(child.transform.position);

            tileMapNodes[(int)child.transform.position.x,(int)child.transform.position.y].isObstacle=true;
        }
        //Debug.Log();
        //GameObject tilemap = test.transform.Find("Tilemap").gameObject;


        //foreach (var position in tilemap.cellBounds.allPositionsWithin)
        //{
        //    if (!tilemap.HasTile(position))
        //    {
        //        continue;
        //    }
        //
        //    // Tile is not empty; do stuff
        //}
        //for (int i = 0; i < gridSize.x; i++)
        //{
        //    for (int j = 0; j < gridSize.y; j++)
        //    {
        //        tileMapNodes[i, j].isObstacle = false;
        //    }
        //}

        //Node startPoint = tileMapNodes[0, 0];
        //Node endPoint = tileMapNodes[gridSize.x - 1, gridSize.y - 1];
        //List<Node> path = SolveAStar(startPoint, endPoint);
        //foreach (var item in path)
        //{
        //    Debug.Log(item.x);
        //}

        //Node startPoint = tileMapNodes[0, 0];
        //Node endPoint = tileMapNodes[9,9];
        //List<Node> path = SolveAStar(startPoint, endPoint);
        //foreach (var item in path)
        //{
        //    Debug.Log("_______");
        //    Debug.Log(item.x);
        //    Debug.Log(item.y);
        //    Debug.Log("_______");
        //}





    }   
    public void ResetNodes()
    {
        //NOT FINISHED!!!
        //tilesmap =  new int[gridSize.x,gridSize.y]{
        //        0,
        //};
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                tileMapNodes[i, j].isVisited = false;
            }
        }


    }

    private void Start()
    {
        InitTileMapPathfinding();
    }

    private void Update()
    {

    }
    private float CalculateDistance(Node a, Node b){
        var distance = Math.Sqrt((Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2)));
        return (float)distance;

    }
    
    public List<Node> SolveAStar(Node startPoint, Node endPoint){
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                tileMapNodes[i, j].globalGoal = float.MaxValue;
                tileMapNodes[i, j].localGoal = float.MaxValue;
                tileMapNodes[i, j].parent = null;
                tileMapNodes[i, j].isVisited = false;
            }
        }
        Node nodeCurrent = startPoint;
        startPoint.localGoal =0.0f;
        startPoint.globalGoal = CalculateDistance(startPoint,endPoint);

        List<Node> listNotTestedNodes = new List<Node>();
        listNotTestedNodes.Add(startPoint);
        while((listNotTestedNodes.Count !=0) && (nodeCurrent != endPoint)){
            listNotTestedNodes  = listNotTestedNodes.OrderBy(o => o.globalGoal).ToList();

            while((listNotTestedNodes.Count!=0) && listNotTestedNodes.First().isVisited){
                listNotTestedNodes.Remove(listNotTestedNodes.First());
            } 
            if(listNotTestedNodes.Count==0){
                break;
            }
            nodeCurrent = listNotTestedNodes.First();
            nodeCurrent.isVisited=true;

            foreach (var item in nodeCurrent.neighBours)
            {
                if(!item.isVisited && !item.isObstacle){
                    listNotTestedNodes.Add(item);
                }
                float posiblyLowerGoal = nodeCurrent.localGoal +CalculateDistance(nodeCurrent,item);

                if(posiblyLowerGoal<item.localGoal){
                    item.parent = nodeCurrent;
                    item.localGoal = posiblyLowerGoal;
                    item.globalGoal = item.localGoal + CalculateDistance(item,endPoint);
                }
            }
        }

        //ResetNodes();
        List<Node> path = new List<Node>();
        if(endPoint!=null){
            Node temp = endPoint;
            while(temp.parent!=null){
                path.Add(temp);
                temp= temp.parent;
            }
        }
        ResetNodes();
        return path;
    }
}
