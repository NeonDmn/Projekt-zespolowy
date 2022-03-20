using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private ContactFilter2D selectable;

    private LineRenderer lineRenderer;
    private BoxCollider2D boxColl;
    private Vector2 initialMousePosition, currentMousePosition;

    private bool inputActionDown = false;
    private bool inputSelectDown = false;
    private bool shiftSelectingActive = false;

    private bool isDragging = false;

    private void Awake()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        boxColl = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (inputSelectDown)
        {
            currentMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (isDragging)
            {
                UpdateSelectionBox();
                UpdateSelectedUnits();
                DrawSelectionVisual();
            }
            else
            {
                if (isDragging = CheckForDrag())
                {
                    EnableSelectionVisuals(true);
                }
            }
        }
    }

    private bool CheckForDrag()
    {
        Vector2 dragDelta = new Vector2(
            Mathf.Abs(initialMousePosition.x - currentMousePosition.x),
            Mathf.Abs(initialMousePosition.y - currentMousePosition.y)
            );
        return (dragDelta.x > 0.1f || dragDelta.y > 0.1f);
    }

    public void ActionInput(InputAction.CallbackContext ctx)
    {
        inputActionDown = ctx.ReadValueAsButton();

        if (inputActionDown && ctx.started)
        {
            initialMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + " select");
                if (Selection.Instance.unitsSelected.Count > 0)
                {
                    Selection.Instance.PathFindAllSelected(initialMousePosition);
                }

            }
        }
    }

    /*
     * Input s�u��cy do zaznaczania pojedynczych jednostek i budynk�w, oraz wielu jednostek 
     * przeci�gaj�c kursor po ekranie.
     */
    public void SelectInput(InputAction.CallbackContext ctx)
    {
        inputSelectDown = ctx.ReadValueAsButton();
        initialMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Physics2D.Raycast(initialMousePosition, Vector2.zero, selectable, hits);

        if (inputSelectDown && ctx.started)
        {
            if (hits.Count > 0)
            {
                // Natrafili�my na co� co mo�na nacisn��
                // Czy trzymamy shift?
                if (shiftSelectingActive)
                {
                    // Tak
                    foreach (var hit in hits)
                    {
                        Selection.Instance.ShiftClickSelect(hit.collider.gameObject);
                    }
                }
                else
                {
                    // Nie
                    foreach (var hit in hits)
                    {
                        Selection.Instance.ClickSelect(hit.collider.gameObject);
                    }
                }
            }
            else if (!shiftSelectingActive)
            {
                Selection.Instance.DeselectAll();
            }
        }

        // Ko�czymy zaznaczanie
        if (!inputSelectDown && isDragging)
        {
            EnableSelectionVisuals(false);
            isDragging = false;
        }
    }

    public void ShiftSelectInput(InputAction.CallbackContext ctx)
    {
        shiftSelectingActive = ctx.ReadValueAsButton();
    }

    private void EnableSelectionVisuals(bool value)
    {
        if (value)
        {
            lineRenderer.positionCount = 4;

            lineRenderer.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(1, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(2, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(3, new Vector2(initialMousePosition.x, initialMousePosition.y));

            boxColl.enabled = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            boxColl.enabled = false;
            lineRenderer.positionCount = 0;
            transform.position = Vector3.zero;
        }
    }

    private void UpdateSelectionBox()
    {
        transform.position = (currentMousePosition + initialMousePosition) / 2;

        boxColl.size = new Vector2(
            Mathf.Abs(initialMousePosition.x - currentMousePosition.x),
            Mathf.Abs(initialMousePosition.y - currentMousePosition.y)
            );
    }

    private void UpdateSelectedUnits()
    {
        // P�tla przez wszystkie jednostki na mapie
        foreach (var unit in Selection.Instance.unitList)
        {
            // Czy jednostka jest zaznaczona?
            if (boxColl.OverlapPoint(unit.transform.position))
            {
                // Tak, jest
                Selection.Instance.TrySelect(unit.gameObject);
            }
            else if (!shiftSelectingActive)
            {
                // Nie, nie jest
                Selection.Instance.TryDeselect(unit.gameObject);
            }
        }
    }

    private void DrawSelectionVisual()
    {
        lineRenderer.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
        lineRenderer.SetPosition(1, new Vector2(initialMousePosition.x, currentMousePosition.y));
        lineRenderer.SetPosition(2, new Vector2(currentMousePosition.x, currentMousePosition.y));
        lineRenderer.SetPosition(3, new Vector2(currentMousePosition.x, initialMousePosition.y));
    }

    public Vector2 GetMousePos()
    {
        return currentMousePosition;
    }
}
