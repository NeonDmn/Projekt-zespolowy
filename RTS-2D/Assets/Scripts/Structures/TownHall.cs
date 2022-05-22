using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TownHall : Structure
{
    public GameObject workerPrefab;
    public Transform workerSpawnPos;
    public ResourceManager resources { get; private set; }

    public UnitsManager units { get; private set; }
    private float workerCreationTime;
    private bool workerTimerRunning = false;

    public bool isWorkerTimerRunning() { return workerTimerRunning;}

    public override void Start()
    {
        resources = new ResourceManager();
        units = new UnitsManager();
    }

    public void CreateWorker()
    {
        if (!resources.TakeFood(1))
        {
            // Nie można utworzyć workera
            // error
        }
        else
        {
            // Można tworzyć
            // timer start
            workerCreationTime = 3f;
            workerTimerRunning = true;
        }
    }

    private void SpawnWorker()
    {
        GameObject obj = Instantiate(workerPrefab, workerSpawnPos.position, Quaternion.identity);
        units.AddWorker(obj.GetComponent<Worker>());
    }

    private void Update()
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
}
