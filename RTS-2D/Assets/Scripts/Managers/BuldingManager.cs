using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuldingManager
{
    // Start is called before the first frame update
    List<Barracks> barracksList = new List<Barracks>();
    List<Farm> farmList = new List<Farm>();
    List<Storage> storageList = new List<Storage>();

    public void AddBarracks(Barracks b)
    {
        barracksList.Add(b);
    }
    public void AddStorage(Storage s)
    {
        storageList.Add(s);
    }
    public void AddFarm(Farm s)
    {
        farmList.Add(s);

    }
    public List<Barracks> GetBarraksList()
    {
        return barracksList;
    }
    public List<Farm> GetFarmList()
    {
        return farmList;
    }
    public List<Storage> GetStorageList()
    {
        return storageList;
    }
    public void AddBulding(GameObject bulding)
    { }

}
