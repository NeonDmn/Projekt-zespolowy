using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public static Color teamColorFriendly = Color.blue;
    public static Color teamColorEnemy = Color.red;

    public enum Team
    {
        Friendly,
        Enemy
    }

    [SerializeField]
    private Team _team;
    public Team team { get { return _team; } }

    public void SetTeam(PlayerTeam.Team team)
    {
        _team = team;
    }

}
