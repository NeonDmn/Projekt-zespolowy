using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour
{
    [SerializeField] private ContactFilter2D selectable;
    [SerializeField] BuildWidget bWidget;

    private LineRenderer lineRenderer;
    private BoxCollider2D boxColl;
    private Vector2 initialMouseWorldPosition, currentMouseWorldPosition;

    private bool inputActionDown = false;
    private bool inputSelectDown = false;
    private bool shiftSelectingActive = false;


    private bool isDragging = false;
    static public bool collide = false;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        boxColl = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        EventManager.OnBuildingModeStarted += Event_OnBuildingStarted;
        EventManager.OnBuildingModeEnded += Event_OnBuildingEnded;
    }
    void Update()
    {
        if (inputSelectDown)
        {
            currentMouseWorldPosition = GetMouseWorldPos();

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
            Mathf.Abs(initialMouseWorldPosition.x - currentMouseWorldPosition.x),
            Mathf.Abs(initialMouseWorldPosition.y - currentMouseWorldPosition.y)
            );
        return (dragDelta.x > 0.1f || dragDelta.y > 0.1f);
    }

    public void ActionInput(InputAction.CallbackContext ctx)
    {
        inputActionDown = ctx.ReadValueAsButton();

        if (inputActionDown && ctx.started)
        {
            initialMouseWorldPosition = GetMouseWorldPos();

            RaycastHit2D hit = Physics2D.Raycast(initialMouseWorldPosition, Vector2.zero);

            if (hit.collider != null && (hit.collider.CompareTag("Structures") || hit.collider.CompareTag("TownHall") || hit.collider.CompareTag("Resource") || hit.collider.CompareTag("Unit")))
            {
                /// Budowanie
                Debug.Log("Nie można zbudować w tym miejscu. Wykryto: " + hit.collider.gameObject.name);
                collide = false;
                if (Selection.Instance.unitsSelected.Count > 0)
                {
                    // Selection.Instance.PathFindAllSelected(initialMousePosition);

                    Selection.Instance.HandleActionBySelected(initialMouseWorldPosition, hit.transform.gameObject);
                }
            }
            else if (hit.collider != null)
            {
                Debug.Log("Można budować");
                Debug.Log(hit.collider.gameObject.name + " select");
                ///
                if (Selection.Instance.unitsSelected.Count > 0)
                {
                    // Selection.Instance.PathFindAllSelected(initialMousePosition);

                    Selection.Instance.HandleActionBySelected(initialMouseWorldPosition, hit.transform.gameObject);
                }
                collide = true;
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
        initialMouseWorldPosition = GetMouseWorldPos();

        if (inputSelectDown && ctx.started)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            Physics2D.Raycast(initialMouseWorldPosition, Vector2.zero, selectable, hits);

            var hitsSelectable = hits.FindAll(
                // Zaznacz obiekty które mogą być zaznaczone
                h => h.collider.GetComponent<Selectable>()
                // Zaznacz tylko, jeśli w twojej drużynie
                && h.collider.GetComponent<PlayerTeam>().team == PlayerTeam.Team.Friendly
                );

            // Zaznacz pierwszy obiekt na liście
            if (hitsSelectable.Count <= 0 || !TrySelectCollider(hitsSelectable[0].collider))
            {
                if (!shiftSelectingActive)
                {
                    // Nie natrafiliśmy na nic zaznaczalnego
                    // Odznaczamy wszystko
                    Selection.Instance.DeselectAll();
                }
            }

        }

        // Ko�czymy zaznaczanie z przytrzymaniem
        if (!inputSelectDown && isDragging)
        {
            EnableSelectionVisuals(false);
            isDragging = false;
        }
    }

    private bool TrySelectCollider(Collider2D obj)
    {
        if (obj != null)
        {
            // Natrafili�my na co� co mo�na zaznaczyć
            // Czy trzymamy shift?
            if (shiftSelectingActive)
            {
                // Tak
                // Nie zaznaczamy budynków przy pomocy shifta
                if (!obj.GetComponent<Structure>())
                {
                    // Zaznaczamy dodatkowe jednostki
                    Selection.Instance.ShiftClickSelect(obj.GetComponent<Selectable>());
                }
                return true;
            }
            else
            {
                // Nie
                // Zaznacz budynek
                if (obj.GetComponent<Structure>())
                {
                    Selection.Instance.SelectStructure(obj.GetComponent<Selectable>());
                    return true;
                }
                else
                {
                    // Zaznacz jednostkę
                    Selection.Instance.ClickSelect(obj.GetComponent<Selectable>());
                    return true;
                }
            }
        }

        return false;
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

            lineRenderer.SetPosition(0, new Vector2(initialMouseWorldPosition.x, initialMouseWorldPosition.y));
            lineRenderer.SetPosition(1, new Vector2(initialMouseWorldPosition.x, initialMouseWorldPosition.y));
            lineRenderer.SetPosition(2, new Vector2(initialMouseWorldPosition.x, initialMouseWorldPosition.y));
            lineRenderer.SetPosition(3, new Vector2(initialMouseWorldPosition.x, initialMouseWorldPosition.y));

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
        transform.position = (currentMouseWorldPosition + initialMouseWorldPosition) / 2;

        boxColl.size = new Vector2(
            Mathf.Abs(initialMouseWorldPosition.x - currentMouseWorldPosition.x),
            Mathf.Abs(initialMouseWorldPosition.y - currentMouseWorldPosition.y)
            );
    }

    private void UpdateSelectedUnits()
    {
        // P�tla przez wszystkie jednostki na mapie
        foreach (var unit in Selection.Instance.unitList)
        {
            if (unit.GetComponent<PlayerTeam>().team == PlayerTeam.Team.Enemy)
                continue;

            // Czy jednostka jest zaznaczona?
            if (boxColl.OverlapPoint(unit.transform.position))
            {
                // Tak, jest
                Selection.Instance.TrySelect(unit.gameObject.GetComponent<Selectable>());
            }
            else if (!shiftSelectingActive)
            {
                // Nie, nie jest
                Selection.Instance.TryDeselect(unit.gameObject.GetComponent<Selectable>());
            }
        }
    }

    private void DrawSelectionVisual()
    {
        lineRenderer.SetPosition(0, new Vector2(initialMouseWorldPosition.x, initialMouseWorldPosition.y));
        lineRenderer.SetPosition(1, new Vector2(initialMouseWorldPosition.x, currentMouseWorldPosition.y));
        lineRenderer.SetPosition(2, new Vector2(currentMouseWorldPosition.x, currentMouseWorldPosition.y));
        lineRenderer.SetPosition(3, new Vector2(currentMouseWorldPosition.x, initialMouseWorldPosition.y));
    }

    /* Building */
    public void Input_Building_Place(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (bWidget.CanBuild())
                FinishBuilding();
        }
    }

    public void Input_Building_Cancel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EndBuildingMode();
        }
    }

    private void Event_OnBuildingStarted(GameObject buildingGO)
    {
        StartBuildingMode(buildingGO);
    }

    private void Event_OnBuildingEnded()
    {
        EndBuildingMode();
    }

    private void StartBuildingMode(GameObject structGO)
    {
        // Zmień tryb kontroli myszy na tryb budowy
        GameManager.instance.SetBuildMouseControlls(true);

        // Załącz podgląd budynku
        bWidget.SetBuilding(structGO);
        bWidget.gameObject.SetActive(true);
    }

    public void FinishBuilding()
    {
        TownHall th = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Friendly);
        Structure str = bWidget.buildingGO.GetComponent<Structure>();

        th.resources.AddToCart(Resource.Type.WOOD, str.woodCost);
        th.resources.AddToCart(Resource.Type.METAL, str.metalCost);

        if (!th.resources.FinalizeTransaction())
        {
            // Nie można budować, brakuje surowców
            Debug.LogWarning("Za mało surowców, by zbudować: " + bWidget.buildingGO.name);
            return;
        }

        // Można budować
        Debug.Log("Rozpoczęto budowę " + bWidget.buildingGO.name);
        GameManager.instance.Build(bWidget.buildingGO, PlayerTeam.Team.Friendly, bWidget.transform.position);
        EventManager.OnBuildingModeEnded?.Invoke();
    }

    private void EndBuildingMode() {

        // Wyłącz podgląd budynku
        bWidget.gameObject.SetActive(false);
        // Zmień tryb kontroli myszy na tryb gry
        GameManager.instance.SetBuildMouseControlls(false);
    }

    public static Vector2 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
