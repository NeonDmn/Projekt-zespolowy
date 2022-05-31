using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Storage : Structure
{
    public override void EventManager_OnBuildingFinished(Structure str)
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxResource(Resource.Type.CRYSTAL, 20);
        th.resources.AddToMaxResource(Resource.Type.WOOD, 75);
        th.resources.AddToMaxResource(Resource.Type.METAL, 50);
    }

    public override void EventManager_OnBuildingDestroyed(ObjectHealth oh)
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxResource(Resource.Type.CRYSTAL, -20);
        th.resources.AddToMaxResource(Resource.Type.WOOD, -75);
        th.resources.AddToMaxResource(Resource.Type.METAL, -50);
    }
}
