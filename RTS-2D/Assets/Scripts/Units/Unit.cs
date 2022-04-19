using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    protected Vector2 endPoint;

    private GridController gridController;
    private Rigidbody2D body;

    // Resources
    public UnityAction<Resource> enteredResourceRange;
    public UnityAction leftResourceRange;

    // Structures
    public UnityAction enteredStructureRange;
    public UnityAction leftStructureRange;

    // Units
    public UnityAction enteredUnitRange;
    public UnityAction leftUnitRange;

    public UnityAction enteredTownHall;
    private int pathIndex;


    public UnitTask currentTask { get; private set; }
    private List<Node> path;
    private Vector3 pos;

    protected void Start()
    {

        GameObject it = GameObject.Find("GridController");
        body = this.GetComponent<Rigidbody2D>();
        gridController = it.GetComponent<GridController>();

        Selection.Instance.unitList.Add(this.gameObject);
        endPoint = transform.position;
        sprRenderer = GetComponent<SpriteRenderer>();

        currentTask = new IdleTask(this);
        
    }
    public void SwitchTask(UnitTask newTask)
    {
        currentTask?.OnTaskEnd();
        currentTask = newTask;
        newTask.OnTaskStart();
    }

    public virtual void ToDo() { }
    public virtual void HandleAction(Vector2 mousePos, GameObject go)
    {
        endPoint = mousePos;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger tag: " + other.tag);

        if (other.tag == "Resource")
            enteredResourceRange?.Invoke(other.gameObject.GetComponent<Resource>());

        if (other.tag == "TownHall")
            enteredTownHall?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Resource")
            leftResourceRange?.Invoke();
    }


    private void Update()
    {
        if(path!=null){
            //Debug.Log(transform.position);

            Debug.Log("______");
            Debug.Log(path[pathIndex].x);
            Debug.Log(path[pathIndex].y);
            Debug.Log("______");
            UnitMovement(path[pathIndex]);
            if((pos.x == path[pathIndex].x) &&(pos.y == path[pathIndex].y)){
                if(pathIndex>0){
                pathIndex = pathIndex-1;
                }
            }
        }
       /// currentTask.Tick();
        //UnitMovement();
        //ToDo();
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

    public void Goto(Vector3 location)
    {
        endPoint = location;
    }

    public void GotoAndSwitchToIdle(Vector3 location)
    {
        if (!(currentTask is IdleTask))
            SwitchTask(new IdleTask(this));
        Goto(location);
    }

    public void CreatePath(Vector2 mousePos)
    {

        gridController = GameObject.Find("GridController").GetComponent<GridController>();
        Node startPointLocal = GridController.tileMapNodes[(int)transform.position.x, (int)transform.position.y];
        Node endPointLocal = GridController.tileMapNodes[(int)mousePos.x,(int)mousePos.y];
        path = gridController.SolveAStar(startPointLocal,endPointLocal);
        Debug.Log(path.Count);
        pathIndex = path.Count-1;
    }

    public void UnitMovement(Node _to)
    {
        pos = Vector3.MoveTowards(transform.position, new Vector3(_to.x,_to.y, 0), 15 * Time.deltaTime);
        GameManager.instance.sendNotification(pos.ToString());
        body.MovePosition(pos);
    }
    


}
