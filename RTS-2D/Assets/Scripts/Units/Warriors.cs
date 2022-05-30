using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warriors : Unit
{

    new private void Start()
    {
        base.Start();
        
    }

    public override void HandleAction(Vector2 mousePos, GameObject go)
    {
        base.HandleAction(mousePos, go);

        Structure str;
        Unit u;
        if (go.GetComponent<PlayerTeam>()) {
            switch (go.GetComponent<PlayerTeam>().team)
            {
                case PlayerTeam.Team.Friendly:
                    // Friendly 
                    Debug.Log("Friendly");
                    SwitchTask(new AttackTask(this, go.GetComponent<ObjectHealth>()));
                    break;

                case PlayerTeam.Team.Enemy:
                    // Enemy 
                    Debug.Log("Enemy");
                    SwitchTask(new AttackTask(this, go.GetComponent<ObjectHealth>()));
                    break;
            }
        }
        else {
            // Pathfinding
            GotoAndSwitchToIdle(mousePos);
        }
    }
}
