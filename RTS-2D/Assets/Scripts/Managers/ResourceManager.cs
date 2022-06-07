using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<Resource.Type, int> resources = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> resourcesMAX = new Dictionary<Resource.Type, int>();
    Dictionary<Resource.Type, int> resourceCart = new Dictionary<Resource.Type, int>();

    int foodInUse;
    int foodMAX;

    int woodInUse;
    int woodMAX;

    int metalInUse;
    int metalMAX;

    public int getMaxResource(Resource.Type type)
    {
        return resourcesMAX[type];

    }
    public ResourceManager()
    {
        resources.Add(Resource.Type.WOOD, 100);
        resources.Add(Resource.Type.METAL, 0);
        resources.Add(Resource.Type.CRYSTAL, 50);

        resourcesMAX.Add(Resource.Type.WOOD, 150);
        resourcesMAX.Add(Resource.Type.METAL, 50);
        resourcesMAX.Add(Resource.Type.CRYSTAL, 100);

        foodMAX = 50;
        foodInUse = 0;

        //GameManager.instance.UIManager.UpdateUIResourceDisplay();
    }

    public int Add(Resource.Type type, int count)
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
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
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
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
        return true;
    }

    public void ClearCart()
    {
        resourceCart.Clear();
    }
    public void FreeFood(int amount)
    {
        foodInUse -= amount;
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
    }

    public bool TakeFood(int amount)
    {
        if ((foodInUse + amount) > foodMAX) return false;

        foodInUse += amount;
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
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

    public int GetFreeFood()
    {
        return foodMAX - foodInUse;
    }
    public int GetMaxFood()
    {
        return foodMAX;
    }

    public void AddToMaxFood(int amount)
    {
        foodMAX += amount;
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
    }

    public void AddToMaxResource(Resource.Type type, int amount)
    {
        resourcesMAX[type] += amount;
        GameManager.instance.UIManager.UpdateUIResourceDisplay();
    }
}
