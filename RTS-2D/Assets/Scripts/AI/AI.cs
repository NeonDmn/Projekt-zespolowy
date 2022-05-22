using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Start is called before the first frame update
    TownHall townHall;
    public ManageWorkers manageWorkers;
    void Start()
    {

        townHall = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Enemy);

        manageWorkers = new ManageWorkers(townHall);

        Debug.Log(manageWorkers == null);

    }

    // Update is called once per frame
    void Update()
    {
        manageWorkers.WorkerAi();
        manageWorkers.WorkerResourceTask();
    }


}
