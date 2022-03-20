using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceSO res;

    void Start()
    {
        if (res == null) Debug.LogError("No resource scriptable object on position " + transform.position);
    }
}
