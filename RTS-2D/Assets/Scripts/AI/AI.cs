using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Start is called before the first frame update
    TownHall townHall;
    [SerializeField] List<GameObject> targetsBuildingsPrefab;

    [SerializeField] float buildTriger;
    [SerializeField] float radiusOfGathering = 10.0f;

    public ManageWorkers manageWorkers;
    public ManageBuildings manageBuildings;

    float seconds;

    void Start()
    {
        seconds = 0;
        townHall = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Enemy);

        manageWorkers = new ManageWorkers(townHall, radiusOfGathering);

        manageBuildings = new ManageBuildings(townHall, targetsBuildingsPrefab);
        Debug.Log(manageWorkers == null);

    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        manageWorkers.WorkerAi();
        manageWorkers.WorkerResourceTask();
        if (seconds > buildTriger)
        {
            if (!manageBuildings.ChoseTarget())
                seconds = seconds + buildTriger + (float)1.0;
            else
                seconds = 0;
        }
    }


}
