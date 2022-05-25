using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;

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

    //Stats Unit
    public UnitStats unitStats;
    private float attackTimer;

    public UnitTask currentTask { get; private set; }
    NavMeshAgent navMeshAgent;

    private Transform currentTarget;

    List<Point> path;


    protected void Start()
    {
        
        //GameObject it = GameObject.Find("GridController");
        body = this.GetComponent<Rigidbody2D>();
        //gridController = it.GetComponent<GridController>();

        Selection.Instance.unitList.Add(gameObject.GetComponent<Selectable>());
        endPoint = transform.position;
        currentTarget = transform;
        Debug.Log("Current Target start" + currentTarget);
        sprRenderer = GetComponent<SpriteRenderer>();

        currentTask = new IdleTask(this);

        // create source and target points
        //Point _from = new Point(1, 1);
        //Point _to = new Point(10, 10);

        // get path
        // path will either be a list of Points (x, y), or an empty list if no path is found.
        navMeshAgent = GetComponent<NavMeshAgent>();

        var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        attackTimer = unitStats.attackSpeed;
        
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
        currentTask.Tick();
    }

    private void FixedUpdate() {
        UnitMovement();
    }
    private void OnDestroy()
    {
        Selection.Instance.unitList.Remove(gameObject.GetComponent<Selectable>());
    }

    /*
     * Wykonywana gdy jednostka jest zaznaczana przez skrypt Selection
     */
    //private void OnSelect()
    //{
    //    sprRenderer.color = new Color(1f, 0f, 0f, 1f);
    //}

    /*
     * Wykonywana gdy jednostka jest odznaczana przez skrypt Selection
     */
    //private void OnDeselect()
    //{
    //    sprRenderer.color = new Color(1f, 1f, 1f, 1f);
    //}

    public void Goto(Vector3 location)
    {
        endPoint = location;
    }

    public void GoandAttack(Vector3 target, Transform enemy)
    {
        attackTimer += Time.deltaTime;
        currentTarget = enemy;
        navMeshAgent.destination = target;
        Debug.Log("Current Target attack" + currentTarget);
        Debug.Log("Target" + target);
        Goto(target);
        var distance = (transform.position - target).magnitude;

        if(distance <= unitStats.attackRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        if(attackTimer >= unitStats.attackSpeed)
        {
            Debug.Log("Attack");
            GameManager.UnitTakeDamage(this, currentTarget.GetComponent<Unit>());
            attackTimer = 0;
            Debug.Log("Current Target damage" + currentTarget);
        }
    }

    public void TakeDamage(Unit enemy, float damage)
    {
        StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
        if (enemy.unitStats.health < 1)
        {
            Debug.Log("Current Target kill" + currentTarget);
            Debug.Log("Kill");
            Destroy(enemy.GetComponent<GameObject>().gameObject);
        }
        else
        {
            enemy.unitStats.health -= damage;
        }     
    }

    IEnumerator Flasher(Color defaultColor)
    {
        var renderer = GetComponent<Renderer>();
        for (int i = 0; i < 2; i++)
        {
            renderer.material.color = Color.gray;
            yield return new WaitForSeconds(.05f);
            renderer.material.color = defaultColor;
            yield return new WaitForSeconds(.05f);
        }
    }

    public void GotoAndSwitchToIdle(Vector3 location)
    {
        if (!(currentTask is IdleTask))
            SwitchTask(new IdleTask(this));
        Goto(location);
    }

    // public void CreatePath(Point _from, Point _to, Vector2 mousePos)
    // {
    //     endPoint = mousePos;
    //     path = Pathfinding.FindPath(gridController.grid, _from, _to);
    //     foreach (var it in path)
    //     {
    //         // Debug.Log(it.x);
    //     }
    // }

    public void UnitMovement()
    {
        navMeshAgent.SetDestination(endPoint);
    }
}
