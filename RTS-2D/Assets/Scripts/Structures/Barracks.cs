using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Structure
{
    private void Start()
    {
        OnBuildingFinished();
    }
    public override void OnBuildingFinished()
    {

    }

    public override void OnBuildingDestroyed()
    {

    }
}
