using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }

    //public GameObject dynamicNavmeshParentObject;
    public NavMeshSurface navMeshSurface;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(_instance);
        }

        _instance = this;

        InitTownHalls();
        Debug.Log("TownHalls found: " + townHalls.Count);

        navMeshSurface.hideEditorLogs = true;

        EventManager.OnTownHallDestroyed += EventManager_Handle_OnTownhallDestroy;
    }
    #endregion

    private void Update()
    {
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }


    [SerializeField] GameObject storagePrefab;
    [SerializeField] GameObject farmPrefab;
    [SerializeField] GameObject barracksPrefab;

    [SerializeField] PlayerInput mouseInput;
    [Space]
    public GameUIManager UIManager;


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

    public static GameObject Build(GameObject go, PlayerTeam.Team team, Vector2 position)
    {
        GameObject b = Instantiate(go, position, Quaternion.identity);
        b.GetComponent<PlayerTeam>().SetTeam(team);
        return b;
    }

    private void EventManager_Handle_OnTownhallDestroy(PlayerTeam.Team team)
    {
        EventManager.OnTownHallDestroyed -= EventManager_Handle_OnTownhallDestroy;

        switch(team)
        {
            case PlayerTeam.Team.Friendly:
            InitLose();
            break;
            
            case PlayerTeam.Team.Enemy:
            InitWin();
            break;
        }
    }

    private void InitWin()
    {
        Debug.LogError("Win!");

        Time.timeScale = 0;
        UIManager.ShowWinScreen();
    }

    private void InitLose()
    {
        Debug.LogError("Lose!");

        Time.timeScale = 0;
        UIManager.ShowLoseScreen();
    }

    public void GotoMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }
}
