using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newResource", menuName = "Resource")]
public class ResourceSO : ScriptableObject
{
    public Sprite sprite;
    public int yield;
    public float gatheringTime;
    public Resource.Type type;
}
