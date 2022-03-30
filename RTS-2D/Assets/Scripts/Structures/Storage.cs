using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Structure
{
    private void Start()
    {
        OnBuildingFinished();
    }
    public override void OnBuildingFinished()
    {
        ResourceManager.Instance.AddToMaxResource(Resource.Type.CRYSTAL, 20);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.WOOD, 75);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.METAL, 50);
    }

    public override void OnBuildingDestroyed()
    {
        ResourceManager.Instance.AddToMaxResource(Resource.Type.CRYSTAL, -20);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.WOOD, -75);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.METAL, -50);
    }
}
