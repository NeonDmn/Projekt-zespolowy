using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitTask
{
    protected Unit owner;
    public UnitTask(Unit parent)
    {
        owner = parent;
    }
    public abstract void OnTaskStart();
    public abstract void Tick();
    public abstract void OnTaskEnd();
}
