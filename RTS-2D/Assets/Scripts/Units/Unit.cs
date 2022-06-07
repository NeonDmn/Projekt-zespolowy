using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    // Navigation
    protected Vector2 endPoint;

    // Resources
    public UnityAction<Resource> enteredResourceRange;
    public UnityAction leftResourceRange;

    // Structures
    //public UnityAction enteredStructureRange;
    //public UnityAction leftStructureRange;

    // Units
    //public UnityAction enteredUnitRange;
    //public UnityAction leftUnitRange;

    public UnityAction enteredTownHall;
    
    //Stats Unit
    public UnitStats unitStats;
    public AudioManager audioManager;

    public UnitTask currentTask { get; private set; }
    protected NavMeshAgent navMeshAgent;

    protected void Start()
    {
        // Dodaj do listy wszystkich jednostek, by można było użyć zaznaczania boxem
        var sel = gameObject.GetComponent<Selectable>();
        if (sel) Selection.Instance.unitList.Add(gameObject.GetComponent<Selectable>());

        // Navmesh init
        navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.updateRotation = false;
		navMeshAgent.updateUpAxis = false;

        // Nie ruszaj się z miejsca po spawnie
        endPoint = transform.position;

        // Audio init
        audioManager = GetComponent<AudioManager>();
        audioManager.setAttack(unitStats.audioClip[2]);
        audioManager.setDeath(unitStats.audioClip[1]);

        // Ustaw życie dla jednostki
        GetComponent<ObjectHealth>().setMaxHealth(unitStats.health);

        // Nic nie rób na początku
        currentTask = new IdleTask(this);
    }

    private void OnDestroy() {
     
        if(GetComponent<PlayerTeam>().team==PlayerTeam.Team.Friendly){
        string unit ="";
        if(gameObject.name == "Blue_Warrior_Melee(Clone)"){
            unit = "WARRIOR";
        }else if(gameObject.name =="Worker(Clone)"){
            unit = "WORKER";
        }else if(gameObject.name == "Blue_Warrior_Ranged(Clone)"){
            unit = "ARCHER";
        }else if(gameObject.name == "Blue_Scout(Clone)"){
            unit = "SCOUT";
        }
        string msg = unit + " DIED!";
        GameManager.instance.UnitDeathMessage(new Vector3(transform.position.x, transform.position.y, transform.position.z), msg);
        }
        SwitchTask(null);
        Selection.Instance.unitList.Remove(gameObject.GetComponent<Selectable>());
    }

    public void SwitchTask(UnitTask newTask)
    {
        currentTask?.OnTaskEnd();
        currentTask = newTask;
        if(newTask != null) newTask.OnTaskStart();
        else Debug.LogWarning("Set to null");
    }

    public virtual void HandleAction(Vector2 mousePos, GameObject go) {}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entered trigger tag: " + other.tag);

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


    public void Update()
    {
        currentTask.Tick();
    }
    private void FixedUpdate() {
        UnitMovement();
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

    public void UnitMovement()
    {
        navMeshAgent.SetDestination(endPoint);
    }

    public void SetStoppingDistance(float distance)
    {
        navMeshAgent.stoppingDistance = distance;
    }
}
