using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warriors : Unit
{
    public override void HandleAction(Vector2 mousePos, GameObject go)
    {
        base.HandleAction(mousePos, go);

        var teamComp = go.GetComponent<PlayerTeam>();
        if (teamComp && teamComp.team != PlayerTeam.Team.Friendly)
        {
            SwitchTask(new AttackTask(this, go.GetComponent<ObjectHealth>()));
        }
        else {
            // Pathfinding
            GotoAndSwitchToIdle(mousePos);
        }
    }
}
