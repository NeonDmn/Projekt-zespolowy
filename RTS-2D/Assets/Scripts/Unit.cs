using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class Unit : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    protected Vector2 endPoint;
    protected GameObject resourceGoal;

    private GridController gridController;
    private Rigidbody2D body;

    // Resources
    protected UnityAction<Resource> enteredResourceRange;
    protected UnityAction leftResourceRange;

    // Structures
    protected UnityAction enteredStructureRange;
    protected UnityAction leftStructureRange;

    // Units
    protected UnityAction enteredUnitRange;
    protected UnityAction leftUnitRange;

    protected UnityAction enteredTownHall;


    List<Point> path;
    protected void Start()
    {

        GameObject it = GameObject.Find("GridController");
        body = this.GetComponent<Rigidbody2D>();
        gridController = it.GetComponent<GridController>();

        Selection.Instance.unitList.Add(this.gameObject);
        endPoint = transform.position;
        sprRenderer = GetComponent<SpriteRenderer>();

        // create source and target points
        //Point _from = new Point(1, 1);
        //Point _to = new Point(10, 10);

        // get path
        // path will either be a list of Points (x, y), or an empty list if no path is found.
    }
    public virtual void ResourceInteraction(GameObject gm) { Debug.LogError("Unit nie dziala"); }
    public virtual void StructureInteraction() { }
    public virtual void UnitInteraction() { }
    public virtual void OnTriggered(Collider2D other) { }
    public virtual void ToDo() { }
    public virtual void HandleAction(Vector2 mousePos, GameObject go)
    {
        endPoint = mousePos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigered tag :" + other.tag);
        //OnTriggered(other);
        if (other.gameObject.Equals(resourceGoal))
            if (other.tag == "Resource")
                enteredResourceRange?.Invoke(other.gameObject.GetComponent<Resource>());

        if (other.gameObject.tag == "TownHall")
        {
            enteredTownHall?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Resource")
            leftResourceRange?.Invoke();
    }


    private void Update()
    {
        UnitMovement();
        ToDo();

    }
    private void OnDestroy()
    {
        Selection.Instance.unitList.Remove(this.gameObject);
    }

    /*
     * Wykonywana gdy jednostka jest zaznaczana przez skrypt Selection
     */
    private void OnSelect()
    {
        sprRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    /*
     * Wykonywana gdy jednostka jest odznaczana przez skrypt Selection
     */
    private void OnDeselect()
    {
        sprRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void CreatePath(Point _from, Point _to, Vector2 mousePos)
    {
        endPoint = mousePos;
        path = Pathfinding.FindPath(gridController.grid, _from, _to);
        foreach (var it in path)
        {
            // Debug.Log(it.x);
        }

    }

    public void UnitMovement()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(endPoint.x, endPoint.y, 0), 15 * Time.deltaTime);
        body.MovePosition(pos);
    }


}
