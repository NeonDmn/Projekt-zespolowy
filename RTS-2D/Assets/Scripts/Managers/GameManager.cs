using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    [SerializeField] GameObject storagePrefab;
    [SerializeField] GameObject farmPrefab;
    [SerializeField] GameObject barracksPrefab;

    [SerializeField] PlayerInput mouseInput;


    //GameObject townHall;
    Dictionary<PlayerTeam.Team, TownHall> townHalls = new Dictionary<PlayerTeam.Team, TownHall>();

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

    public void SetBuildMouseControlls(bool isBuilding)
    {
        if (isBuilding)
            mouseInput.SwitchCurrentActionMap("Building");
        else
            mouseInput.SwitchCurrentActionMap("Cursor");
    }

    public TownHall GetTownHallObject(PlayerTeam.Team team)
    {
        return townHalls[team];
    }

    public void BuildStorage()
    {
        EventManager.OnBuildingModeStarted?.Invoke(storagePrefab);
    }

    public void BuildFarm()
    {
        EventManager.OnBuildingModeStarted?.Invoke(farmPrefab);
    }

    public void BuildBarracks()
    {
        EventManager.OnBuildingModeStarted?.Invoke(barracksPrefab);
    }

    public void Build(GameObject go, PlayerTeam.Team team, Vector2 position)
    {
        GameObject b = Instantiate(go, position, Quaternion.identity);
        b.GetComponent<PlayerTeam>().SetTeam(team);
    }
}
