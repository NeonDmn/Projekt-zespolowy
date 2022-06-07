using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TownHall : Structure
{
    public GameObject workerPrefab;
    public Transform workerSpawnPos;
    public ResourceManager resources { get; private set; }

    public UnitsManager units { get; private set; }

    private float workerCreationTime;
    private bool workerTimerRunning = false;

    public bool isWorkerTimerRunning() { return workerTimerRunning; }

    private void Awake()
    {
        resources = new ResourceManager();
        units = new UnitsManager();

        
    }
    public override void Start()
    {
        GetComponent<ObjectHealth>().setMaxHealth(life);
        GetComponent<ObjectHealth>().onObjectDie += SendEvent_OnTownhallDestroyed;
    }

    public void CreateWorker()
    {
        resources.AddToCart(Resource.Type.CRYSTAL, 5);
        if (resources.GetFreeFood() >= 2 && resources.FinalizeTransaction())
        {
            // Można tworzyć
            // timer start
            resources.TakeFood(2);
            workerCreationTime = 3f;
            workerTimerRunning = true;
        }
        else
        {
            // Nie można utworzyć workera
            // error
        }
    }

    private void SpawnWorker()
    {
        GameObject obj = Instantiate(workerPrefab, workerSpawnPos.position, Quaternion.identity);
        units.AddWorker(obj.GetComponent<Worker>());
    }

    public override void Update()
    {
        //timer 
        if (workerTimerRunning)
        {
            if (workerCreationTime > 0)
            {
                workerCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnWorker();
                workerTimerRunning = false;
            }
        }
    }

    private void OnSelect()
    {
        GameManager.instance.UIManager.ShowBuildMenu();
    }

    private void OnDeselect()
    {
        GameManager.instance.UIManager.HideBuildMenu();
    }


    private void SendEvent_OnTownhallDestroyed(ObjectHealth oh)
    {
        oh.onObjectDie -= SendEvent_OnTownhallDestroyed;

        //Instantiate(buildSprite, transform.position, Quaternion.identity);

        EventManager.OnTownHallDestroyed?.Invoke(GetComponent<PlayerTeam>().team);
    }
}
