using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Selection : MonoBehaviour
{
    public List<Selectable> unitList = new List<Selectable>();
    public List<Selectable> unitsSelected = new List<Selectable>();
    private Selectable structureSelected;

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

    public void ClickSelect(Selectable toSelect)
    {
        DeselectAll();
        Select(toSelect);
    }

    public void TrySelect(Selectable toSelect)
    {
        if (!unitsSelected.Contains(toSelect))
        {
            Select(toSelect);
        }
    }

    public void TryDeselect(Selectable toDeselect)
    {
        if (unitsSelected.Contains(toDeselect))
        {
            Deselect(toDeselect);
        }
    }

    public void ShiftClickSelect(Selectable toSelect)
    {
        DeselectStructure();
        if (!unitsSelected.Contains(toSelect))
        {
            Select(toSelect);
        }
        else
        {
            Deselect(toSelect);
        }
    }

    public void DeselectAll()
    {
        DeselectStructure();
        if (unitsSelected.Count < 1) return;

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

    private void Select(Selectable unit)
    {
        unitsSelected.Add(unit);

        unit.SendMessage("OnSelect");
    }

    private void Deselect(Selectable unit)
    {
        //DeselectStructure();
        unitsSelected.Remove(unit);

        unit.SendMessage("OnDeselect");
    }

    public void SelectStructure(Selectable structure)
    {
        // Odznacz wszystkie jednostki gdy zaznaczasz budynek
        DeselectAll();

        // Wiadomo�� odznaczenia dla wcze�niej zaznaczonego budynku (je�li taki by�)
        if (structureSelected) structure.SendMessage("OnDeselect");

        // Ustaw nowy budynek jako zaznaczony i wy�lij sygna� zaznaczenia
        structureSelected = structure;
        structure.SendMessage("OnSelect");
    }

    private void DeselectStructure()
    {
        if (structureSelected) structureSelected.SendMessage("OnDeselect");
        structureSelected = null;
    }
}
