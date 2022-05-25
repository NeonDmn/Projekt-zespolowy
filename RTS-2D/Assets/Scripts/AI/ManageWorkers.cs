using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWorkers
{
    public Resource.Type target;
    TownHall townHall;

    List<Resource> resourcesList = new List<Resource>();

    public void ChoseResource()
    {
        int wood = (int)(((float)townHall.resources.GetResourceCount(Resource.Type.WOOD) / (float)townHall.resources.getMaxResource(Resource.Type.WOOD)) * 100);
        int metal = (int)(((float)townHall.resources.GetResourceCount(Resource.Type.METAL) / (float)townHall.resources.getMaxResource(Resource.Type.METAL)) * 100);
        int crystal = (int)(((float)townHall.resources.GetResourceCount(Resource.Type.CRYSTAL) / (float)townHall.resources.getMaxResource(Resource.Type.CRYSTAL)) * 100);



        Debug.Log("wood " + wood + " crustal " + crystal + " metel " + metal);

        target = Resource.Type.METAL;
        if (wood <= metal)
            target = Resource.Type.WOOD;
        if (crystal <= wood)
            target = Resource.Type.CRYSTAL;


    }
    public ManageWorkers(TownHall hall, float radiusOfGathering)
    {
        this.townHall = hall;
        ChoseResource();
        findAllResources(radiusOfGathering);
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
            Worker currentWorker = townHall.units.GetWorkers()[i];
            if (currentWorker.currentTask is IdleTask)
            {
                // Debug.Log("Worker nr " + i + " jest bez czynny");
                // Debug.Log(ChoseResource());
                //currentWorker.currentTask.;

                Resource r = findResource(target);
                if (r != null)
                {
                    currentWorker.Goto(r.transform.position);
                    currentWorker.SwitchTask(new GatherTask(currentWorker, r));
                    Debug.Log("Do zbierania " + target.ToString());

                }
            }
            //  else
            // Debug.Log(currentWorker.currentTask.GetType().ToString());
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
    private void findAllResources(float radius)
    {
        GameObject[] thls = GameObject.FindGameObjectsWithTag("Resource");
        resourcesList.Clear();
        foreach (GameObject obj in thls)
        {
            Vector3 vecResource = obj.transform.position;
            Vector3 vecTownHall = townHall.transform.position;
            if (Vector3.Distance(vecResource, vecTownHall) <= radius)
                resourcesList.Add(obj.GetComponent<Resource>());

        }
    }
    private Resource findResource(Resource.Type type)
    {

        foreach (Resource r in resourcesList)
        {
            if (r.GetResourceType() == type)
                return r;
        }
        return null;
    }

}
