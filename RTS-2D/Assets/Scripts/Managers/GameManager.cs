using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(_instance);
        }

        _instance = this;

        InitTownHalls();
        Debug.Log("TownHalls found: " + townHalls.Count);
    }
    #endregion


    GameObject storagePrefab;
    GameObject farmPrefab;
    GameObject barracksPrefab;
    //GameObject townHall;
    Dictionary<PlayerTeam.Team, TownHall> townHalls = new Dictionary<PlayerTeam.Team, TownHall>();
    private void Start()
    {
        // InitTownHalls();
        // Debug.Log("TownHalls found: " + townHalls.Count);
    }

    private void InitTownHalls()
    {
        GameObject[] thls = GameObject.FindGameObjectsWithTag("TownHall");
        foreach (var th in thls)
        {
            PlayerTeam.Team thTeam = th.GetComponent<PlayerTeam>().team;
            if (!townHalls.ContainsKey(thTeam))
            {
                townHalls.Add(thTeam, th.GetComponent<TownHall>());
            }
        }
    }

    public TownHall GetTownHallObject(PlayerTeam.Team team)
    {
        return townHalls[team];
    }

    public void BuildStorage()
    {
        //Build(storagePrefab, team, position);
        EventManager.OnBuildingStarted?.Invoke(storagePrefab);
    }

    public void BuildFarm()
    {
        //Build(farmPrefab, team, position);
    }

    public void BuildBarracks()
    {
        //Build(barracksPrefab, team, position);
    }

    public void Build(GameObject go, PlayerTeam.Team team, Vector2 position)
    {
        GameObject b = Instantiate(go, position, Quaternion.identity);
        b.GetComponent<PlayerTeam>().SetTeam(team);
    }
}
