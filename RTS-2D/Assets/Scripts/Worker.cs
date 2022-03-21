using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Unit
{
    //private string resource;
    private float gatherTime = 10.0f;
    private bool timerStart = false;
    private Vector2 townHallPos;
    private Resource currentResource;

    Dictionary<Resource.Type, int> backpack = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> backpackMAX = new Dictionary<Resource.Type, int>();

    private void Start()
    {
        base.Start();

        townHallPos = GameObject.FindGameObjectWithTag("TownHall").transform.position;

        backpack.Add(Resource.Type.WOOD, 0);
        backpack.Add(Resource.Type.METAL, 0);
        backpack.Add(Resource.Type.CRYSTAL, 0);

        backpackMAX.Add(Resource.Type.WOOD, 20);
        backpackMAX.Add(Resource.Type.METAL, 15);
        backpackMAX.Add(Resource.Type.CRYSTAL, 20);

        enteredResourceRange += OnEnteredResource;
        leftResourceRange += OnLeftResource;

        enteredTownHall += DumpResources;
    }

    private void OnDestroy()
    {
        enteredResourceRange -= OnEnteredResource;
        leftResourceRange -= OnLeftResource;
        enteredTownHall -= DumpResources;
    }

    private void OnEnteredResource(Resource resource)
    {
        currentResource = resource;
        Debug.Log("Zagadza się zbieraj !!!!");
        StartGatherTimer();
    }

    private void OnLeftResource()
    {
        currentResource = null;
        StopGatherTimer();
    }

    private void StartGatherTimer()
    {
        gatherTime = currentResource.GetGatherTime();
        timerStart = true;
    }
    private void StopGatherTimer()
    {
        timerStart = false;
    }

    private void DumpResources()
    {
        foreach (var it in backpack)
        {
            if (it.Value < 1) continue;

            int added = ResourceManager.Instance.AddResource(it.Key, it.Value);
            Debug.Log("Do ratusza oddano " + added + " " + it.Key + "!");
        }

        backpack[Resource.Type.WOOD] = 0;
        backpack[Resource.Type.METAL] = 0;
        backpack[Resource.Type.CRYSTAL] = 0;

        endPoint = resourceGoal.transform.position;
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
            resourceGoal = go;
        }
        else if (u = go.GetComponent<Unit>())
        {
            // Check team
            resourceGoal = null;
        }
        else
        {
            // Pathfinding
            resourceGoal = null;
        }
    }

    public override void ToDo()
    {
        if (timerStart)
        {
            if (gatherTime > 0.0f)
            {
                gatherTime -= Time.deltaTime;
            }
            else
            {
                OnGatherSuccess();
            }
        }
    }

    private void OnGatherSuccess()
    {
        AddToBackpack(currentResource.GetResourceType(), currentResource.GetResourceYield());
        StopGatherTimer();
        endPoint = townHallPos;
    }

    private void AddToBackpack(Resource.Type type, int count)
    {

        backpack[type] += count;
        if (backpack[type] > backpackMAX[type])
            backpack[type] = backpackMAX[type];
    }

    public override void ResourceInteraction(GameObject gm)
    {
        //resource = tileTag;

    }
    // public override void OnTriggered(Collider2D other)
    // {

    //     if (other.tag.Equals(resource))
    //     {
    //         Debug.Log("Zagadza się zbieraj !!!!");
    //         resource = "";
    //         timerStart = true;
    //     }
    // }
    public override void StructureInteraction() { }
    public override void UnitInteraction() { }
}
