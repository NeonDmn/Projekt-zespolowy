using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMenu : MonoBehaviour
{
    private Barracks barracks;
    public void Init(Barracks barracks)
    {
        this.barracks = barracks;
    }

    public void SpawnWarrior()
    {
        barracks.CreateMelee();
    }

    public void SpawnArcher()
    {
        barracks.CreateRanged();
    }

    public void SpawnScout()
    {
        barracks.CreateScout();
    }
}
