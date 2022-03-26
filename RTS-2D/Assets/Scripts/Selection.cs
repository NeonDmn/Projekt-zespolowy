using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Selection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static Selection _instance;
    public MouseInputs mouseInputs;
    public static Selection Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {

        DeselectAll();
        Select(unitToAdd);
    }

    public void TrySelect(GameObject unitToSelect)
    {
        if (!unitsSelected.Contains(unitToSelect))
        {
            Select(unitToSelect);
        }
    }

    public void TryDeselect(GameObject unitToSelect)
    {
        if (unitsSelected.Contains(unitToSelect))
        {
            Deselect(unitToSelect);
        }
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            Select(unitToAdd);
        }
        else
        {
            Deselect(unitToAdd);
        }
    }

    public void DeselectAll()
    {
        if (unitsSelected.Count < 1) return;


        foreach (GameObject it in unitsSelected)
        {
            //Debug.Log(it.transform.position);
        }

        foreach (var unit in unitsSelected)
        {
            unit.SendMessage("OnDeselect");
        }
        unitsSelected.Clear();
        //Debug.Log("Deselected ALL units!");
    }
    // public void PathFindAllSelected(Vector2 currentMousePosition)
    // {

    //     foreach (GameObject it in unitsSelected)
    //     {
    //         //Debug.Log("MAJSTER___-");
    //         //Debug.Log(it.transform.position);

    //         Point _from = new Point((int)it.transform.position.x, (int)it.transform.position.y);
    //         Point _to = new Point((int)currentMousePosition.x, (int)currentMousePosition.y);

    //         Unit unit = it.GetComponent<Unit>();
    //         unit.CreatePath(_from, _to, currentMousePosition);

    //     }
    // }

    public void HandleActionBySelected(Vector2 currentMousePosition, GameObject gm)
    {
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<Unit>().HandleAction(currentMousePosition, gm);
        }
    }

    private void Select(GameObject unit)
    {
        unitsSelected.Add(unit);

        unit.SendMessage("OnSelect");
        Debug.Log("Unit " + unit.name + " selected");
    }

    private void Deselect(GameObject unit)
    {
        unit.SendMessage("OnDeselect");
        unitsSelected.Remove(unit);
        // Debug.Log("Unit " + unit.name + " deselected");
    }
}
