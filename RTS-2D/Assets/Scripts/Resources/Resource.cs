using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum Type
    {
        WOOD,
        METAL,
        CRYSTAL
    }
    public ResourceSO res;

    void Start()
    {
        if (res == null) Debug.LogError("No resource scriptable object on position " + transform.position);
    }

    public Resource.Type GetResourceType()
    {
        return res.type;
    }

    public int GetResourceYield()
    {
        return res.yield;
    }

    public float GetGatherTime()
    {
        return res.gatheringTime;
    }
}
