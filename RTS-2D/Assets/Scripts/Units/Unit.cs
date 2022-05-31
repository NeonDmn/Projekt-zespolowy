using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private SpriteRenderer sprRenderer;
    protected Vector2 endPoint;

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
    public AudioManager audioManager;

    public UnitTask currentTask { get; private set; }
    NavMeshAgent navMeshAgent;

    protected void Start()
    {

        //GameObject it = GameObject.Find("GridController");
        body = this.GetComponent<Rigidbody2D>();
        //gridController = it.GetComponent<GridController>();

        Selection.Instance.unitList.Add(gameObject.GetComponent<Selectable>());
        endPoint = transform.position;
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

        GetComponent<ObjectHealth>().setMaxHealth(unitStats.health);
    }
    public void SwitchTask(UnitTask newTask)
    {
        currentTask?.OnTaskEnd();
        currentTask = newTask;
        newTask.OnTaskStart();
    }

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
        if (audioManager.attack.clip == null || audioManager.death.clip == null)
        {
            Debug.Log("Set UNIT AUDIO");
            audioManager.setAttack(unitStats.audioClip[2]);
            audioManager.setDeath(unitStats.audioClip[1]);
        }
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

    public void GotoAndSwitchToIdle(Vector3 location)
    {
        if (!(currentTask is IdleTask))
            SwitchTask(new IdleTask(this));
        Goto(location);
    }

    public void UnitMovement()
    {
        navMeshAgent.SetDestination(endPoint);
    }

    public void SetStoppingDistance(float distance)
    {
        navMeshAgent.stoppingDistance = distance;
    }
}
