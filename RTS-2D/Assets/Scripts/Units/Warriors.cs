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

        Resource res;
        Structure str;
        Unit u;

        if (str = go.GetComponent<Structure>())
        {
            switch (go.GetComponent<PlayerTeam>().team)
            {
                case PlayerTeam.Team.Friendly:
                    // Friendly structure
                    Debug.Log("Friendly structure");
                    GoandAttack(mousePos, go.transform);
                    break;

                case PlayerTeam.Team.Enemy:
                    // Enemy structure
                    Debug.Log("Enemy structure");
                    GoandAttack(mousePos, go.transform);
                    break;
            }
        }
        else if (u = go.GetComponent<Unit>())
        {
            // Check team
            switch (go.GetComponent<PlayerTeam>().team)
            {
                case PlayerTeam.Team.Friendly:
                    // Friendly Unit
                    Debug.Log("Friendly Unit");
                    GoandAttack(mousePos, go.transform);
                    break;

                case PlayerTeam.Team.Enemy:
                    // Enemy Unit
                    Debug.Log("Enemy Unit");
                    GoandAttack(mousePos, go.transform);
                    break;
            }
            
        }
        else
        {
            // Pathfinding
            GotoAndSwitchToIdle(mousePos);
        }
    }

  
}
