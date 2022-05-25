using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBuildings
{
    // Start is called before the first frame update
    TownHall townHall;
    GameObject target;
    List<GameObject> targets;

    public ManageBuildings(TownHall hall, List<GameObject> targets)
    {
        townHall = hall;
        this.targets = targets;
        target = targets[0];
    }
    public void ChoseTarget()
    {
        int wood = townHall.resources.GetResourceFreeCount(Resource.Type.WOOD);
        int crystal = townHall.resources.GetResourceFreeCount(Resource.Type.CRYSTAL);
        int metal = townHall.resources.GetResourceFreeCount(Resource.Type.METAL);

        Structure s = target.GetComponent<Structure>();

        townHall.resources.AddToCart(Resource.Type.WOOD, s.woodCost);
        townHall.resources.AddToCart(Resource.Type.METAL, s.metalCost);

        bool canBuild = townHall.resources.FinalizeTransaction();
        if (canBuild)
        {
            //  GameManager.Build(target,PlayerTeam.Team.Enemy,Ve);
        }
        else
        {
            //Random.value
            int rval = Random.Range(0, targets.Count);
            target = targets[rval];
        }
        //GameManager.Build(target,PlayerTeam.Team.Enemy,);
    }
}
