using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager _instance;
    public static ResourceManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    Dictionary<Resource.Type, int> resources = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> resourcesMAX = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> resourceCart = new Dictionary<Resource.Type, int>();

    int foodInUse;
    int foodMAX;

    int woodInUse;
    int woodMAX;

    int metalInUse;
    int metalMAX;

    private void Start()
    {
        resources.Add(Resource.Type.WOOD, 100);
        resources.Add(Resource.Type.METAL, 0);
        resources.Add(Resource.Type.CRYSTAL, 50);

        resourcesMAX.Add(Resource.Type.WOOD, 150);
        resourcesMAX.Add(Resource.Type.METAL, 50);
        resourcesMAX.Add(Resource.Type.CRYSTAL, 100);

        foodMAX = 50;
        foodInUse = 0;

        woodInUse = 0;
        woodMAX = 0;

        metalMAX = 0;
        metalInUse = 0;
    }

    public int AddResource(Resource.Type type, int count)
    {
        resources[type] += count;//15 +15 =30

        int delta = count;
        if (resources[type] > resourcesMAX[type])
        {//50-30
            delta = resources[type] - resourcesMAX[type];
            //delta=20
            resources[type] = resourcesMAX[type];
            delta = count - delta;
        }
        return delta;
    }

    public void AddToCart(Resource.Type type, int count)
    {
        //if ((resources[type] - count) < 0) return false;

        if (resourceCart.ContainsKey(type))
        {
            resourceCart[type] += count;
        }
        else
        {
            resourceCart.Add(type, count);
        }
    }

    public bool FinalizeTransaction()
    {
        bool failed = false; ;
        foreach (var res in resourceCart)
        {
            if ((resources[res.Key] - res.Value) < 0)
            {
                failed = true;
                break;
            }
        }

        if (failed)
        {
            resourceCart.Clear();
            return false;
        }

        foreach (var res in resourceCart)
        {
            resources[res.Key] -= res.Value;
        }

        resourceCart.Clear();
        return true;
    }

    public void FreeFood(int amount)
    {
        foodInUse -= amount;
    }

    public bool TakeFood(int amount)
    {
        if ((foodInUse + amount) > foodMAX) return false;

        foodInUse += amount;
        return true;
    }


    public bool TakeWood(int amount)
    {
        if ((woodInUse + amount) > woodMAX) return false;

        woodInUse += amount;
        return true;
    }


    public bool TakeMetal(int amount)
    {
        if ((metalInUse + amount) > metalMAX) return false;

        metalInUse += amount;
        return true;
    }

    public int GetResourceCount(Resource.Type type)
    {
        return resources[type];
    }

    public int GetResourceFreeCount(Resource.Type type)
    {
        return resourcesMAX[type] - resources[type];
    }

    public void AddToMaxFood(int amount)
    {
        foodMAX += amount;
    }

    public void AddToMaxResource(Resource.Type type, int amount)
    {
        resourcesMAX[type] += amount;
    }
}
