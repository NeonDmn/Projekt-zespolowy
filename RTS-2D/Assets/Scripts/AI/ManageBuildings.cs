using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBuildings
{
    // Start is called before the first frame update
    TownHall townHall;
    GameObject target;
    List<GameObject> targets;

    int lastBuldings;

    Vector2 targetPos;


    public ManageBuildings(TownHall hall, List<GameObject> targets)
    {
        townHall = hall;
        this.targets = targets;
        lastBuldings = 0;
        target = targets[0];
    }
    bool TryBuild(int radius)
    {
        Vector2 townHallPos = townHall.transform.position;
        int x = Random.Range((int)townHallPos.x - radius, (int)townHallPos.x + radius);
        int y = Random.Range((int)townHallPos.y - radius, (int)townHallPos.y + radius);
        targetPos = new Vector2(x, y);
        return BuildWidget.CanBuild(targetPos);
    }
    public bool ChoseTarget()
    {

        int rval = Random.Range(0, targets.Count);
        bool buidScusess = false;
        //Debug.Log("wood" + townHall.resources.GetResourceCount(Resource.Type.WOOD) + " metal " + townHall.resources.GetResourceCount(Resource.Type.METAL));
        if (rval != lastBuldings)
        {

            target = targets[rval];
            lastBuldings = rval;
            int wood = townHall.resources.GetResourceFreeCount(Resource.Type.WOOD);
            int crystal = townHall.resources.GetResourceFreeCount(Resource.Type.CRYSTAL);
            int metal = townHall.resources.GetResourceFreeCount(Resource.Type.METAL);

            Structure s = target.GetComponent<Structure>();

            townHall.resources.AddToCart(Resource.Type.WOOD, s.woodCost);
            townHall.resources.AddToCart(Resource.Type.METAL, s.metalCost);

            bool canBuild = townHall.resources.FinalizeTransaction();
            int radius = 3;
            bool canSetBuilding = false;
            if (canBuild)
            {
                for (int i = 0; i < 100; i++)
                {

                    if (i == 34)
                        radius = 5;
                    if (i == 74)
                        radius = 9;

                    if (TryBuild(radius))
                    {

                        canSetBuilding = true;
                        break;

                    }
                }
                if (canSetBuilding)
                {
                    GameManager.Build(target, PlayerTeam.Team.Enemy, targetPos);
                    buidScusess = true;

                }
                else
                {
                    townHall.resources.Add(Resource.Type.WOOD, s.woodCost);
                    townHall.resources.Add(Resource.Type.METAL, s.metalCost);
                }
            }
        }
        //Random.value


        return buidScusess;
    }
}
