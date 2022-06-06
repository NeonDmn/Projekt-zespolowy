using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Farm : Structure
{
    [SerializeField] GameObject farmNotificationFinishedPrefab;
    public override void EventManager_OnBuildingFinished(Structure str)
    {
        if(buildingFinished){
        if(GetComponent<PlayerTeam>().team==PlayerTeam.Team.Friendly && !onlyOnce){
           onlyOnce = true;
           var temp =  Instantiate(farmNotificationFinishedPrefab, new Vector3(transform.position.x, (transform.position.y+1.0f), transform.position.z), Quaternion.identity);
           Destroy(temp,3.0f);
        }
        }
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);
        th.resources.AddToMaxFood(50);
    }

    public override void EventManager_OnBuildingDestroyed(ObjectHealth oh)
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxFood(-50);
    }

}
