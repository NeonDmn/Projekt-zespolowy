using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float buildTime;
    public int life;
    public int woodCost;
    public int metalCost;

    public virtual void OnBuildingFinished()
    {

    }

    public virtual void OnBuildingDestroyed()
    {

    }

}
