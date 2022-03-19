using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] public Vector2Int gridSize;
    [SerializeField] public float cellRadius = 0.5f;
    public FlowField currentFlowField;
    public GridDebug gridDebug;


    private void InitFlowField(){
        currentFlowField = new FlowField(cellRadius, gridSize);
        currentFlowField.CreateGrid();
        gridDebug.SetFlowField(currentFlowField);
    }
    private void Update() {
        InitFlowField();
        currentFlowField.CreateCostField();        
    }
}
