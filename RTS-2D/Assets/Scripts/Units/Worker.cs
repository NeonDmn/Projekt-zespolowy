using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    Dictionary<Resource.Type, int> backpack = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> backpackMAX = new Dictionary<Resource.Type, int>();

    new private void Start()
    {
        base.Start();

        backpack.Add(Resource.Type.WOOD, 0);
        backpack.Add(Resource.Type.METAL, 0);
        backpack.Add(Resource.Type.CRYSTAL, 0);

        backpackMAX.Add(Resource.Type.WOOD, 20);
        backpackMAX.Add(Resource.Type.METAL, 15);
        backpackMAX.Add(Resource.Type.CRYSTAL, 20);


    }

    public override void HandleAction(Vector2 mousePos, GameObject go)
    {
        base.HandleAction(mousePos, go);

        Resource res;
        //Structure str;
        Unit u;

        if (res = go.GetComponent<Resource>())
        {
            // Start gather
            SwitchTask(new GatherTask(this, res));
        }
        else if (u = go.GetComponent<Unit>())
        {
            // Check team
            GotoAndSwitchToIdle(mousePos);
        }
        else
        {
            // Pathfinding
            GotoAndSwitchToIdle(mousePos);
        }
    }

    public void AddToBackpack(Resource.Type type, int count)
    {
        backpack[type] += count;
        if (backpack[type] > backpackMAX[type])
            backpack[type] = backpackMAX[type];
    }

    public void ClearBackpack()
    {
        backpack[Resource.Type.WOOD] = 0;
        backpack[Resource.Type.METAL] = 0;
        backpack[Resource.Type.CRYSTAL] = 0;
    }

    public Dictionary<Resource.Type, int> GetBackpackContent()
    {
        return backpack;
    }
}
