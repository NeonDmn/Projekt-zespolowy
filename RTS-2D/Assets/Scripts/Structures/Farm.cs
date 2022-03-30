using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Structure
{
    private void Start()
    {
        OnBuildingFinished();
    }
    public override void OnBuildingFinished()
    {
        ResourceManager.Instance.AddToMaxFood(50);
    }

    public override void OnBuildingDestroyed()
    {
        ResourceManager.Instance.AddToMaxFood(-50);
    }
}
