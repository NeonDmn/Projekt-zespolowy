using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Start is called before the first frame update
    TownHall townHall;
    void Start()
    {
        townHall = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Enemy);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WorkerCreate()
    {
        int food = townHall.resources.GetFreeFood();
        int workerCount = townHall.units.GetWorkers().Count;
        if (food < 50 && workerCount <= 3)
        {
            townHall.CreateWorker();
        }
        else if (food < 100 && food > 50 && workerCount <= 7)
        {
            townHall.CreateWorker();
        }
        if (food < 150 && food > 100 && workerCount <= 10)
        {
            townHall.CreateWorker();
        }


    }

}
