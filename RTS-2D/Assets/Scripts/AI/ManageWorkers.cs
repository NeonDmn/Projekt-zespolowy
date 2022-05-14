using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWorkers
{
    TownHall townHall;
    private Resource.Type ChoseResource()
    {
        int wood = townHall.resources.GetResourceCount(Resource.Type.WOOD);
        int metal = townHall.resources.GetResourceCount(Resource.Type.METAL);
        int crystal = townHall.resources.GetResourceCount(Resource.Type.CRYSTAL);

        Resource.Type toGet = Resource.Type.WOOD;
        if (wood > metal)
            toGet = Resource.Type.METAL;
        if (metal > crystal)
            toGet = Resource.Type.CRYSTAL;
        return toGet;
    }
    public ManageWorkers(TownHall hall)
    {
        this.townHall = hall;

    }
    public void WorkerAi()
    {
        WorkerCreate();
        WorkerResourceTask();
    }
    public void WorkerResourceTask()
    {
        for (int i = 0; i < townHall.units.GetWorkers().Count; i++)
        {
            if (!townHall.units.GetWorkers()[i].currentTask.isWorking())
            {
                Debug.Log("Worker nr " + i + " jest bez czynny");
                Debug.Log(ChoseResource());
            }
            else
                Debug.Log("nie działa");
        }

    }


    public void WorkerCreate()
    {

        int food = townHall.resources.GetFreeFood();
        int workerCount = townHall.units.GetWorkers().Count;
        bool workerTimerRunning = townHall.isWorkerTimerRunning();
        // Debug.Log("Worker Count : " + workerCount + " Food " + food);
        if (food <= 50 && workerCount < 3 && workerTimerRunning != true)
        {
            //       Debug.Log("Craet Worker!!!");
            townHall.CreateWorker();

        }
        else if (food < 100 && food > 50 && workerCount < 7 && workerTimerRunning != true)
        {
            townHall.CreateWorker();
        }
        if (food < 150 && food > 100 && workerCount < 10 && workerTimerRunning != true)
        {
            townHall.CreateWorker();
        }
        for (int i = 0; i < workerCount; i++)
            townHall.units.GetWorkers()[i].GetComponent<PlayerTeam>().SetTeam(PlayerTeam.Team.Enemy);

    }

}
