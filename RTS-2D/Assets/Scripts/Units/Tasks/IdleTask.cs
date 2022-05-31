using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IdleTask : UnitTask
{
    public IdleTask(Unit parent) : base(parent) { }

    public override void OnTaskStart()
    {
        working = false;
        //
    }

    public override void Tick()
    {
        //
    }

    public override void OnTaskEnd()
    {
        working = true;
    }
}
