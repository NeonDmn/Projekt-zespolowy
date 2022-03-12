using System.Collections.Generic;
using UnityEngine;
public class Selection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static Selection _instance;
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
        foreach (var unit in unitsSelected)
        {
            unit.SendMessage("OnDeselect");
        }
        unitsSelected.Clear();
        Debug.Log("Deselected ALL units!");
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
        Debug.Log("Unit " + unit.name + " deselected");
    }
}
