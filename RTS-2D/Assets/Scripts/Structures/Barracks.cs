using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Barracks : Structure
{
    public GameObject warriorMeleePrefab;
    public GameObject warriorScoutPrefab;
    public GameObject warriorRangedPrefab;
    public Transform warriorSpawnPos;

    float warriorCreationTime = 0.0f;
    bool creatingWarrior = false;
    GameObject warriorToCreate;

    PlayerTeam.Team team;

    public override void Start() {
        base.Start();

        team = GetComponent<PlayerTeam>().team;
    }
    public override void Update()
    {
        base.Update();

        if (creatingWarrior && buildingFinished) 
        {
            if (warriorCreationTime > 0.0f)
            {
                warriorCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnWarrior(warriorToCreate);
                creatingWarrior = false;
            }
        }
    }

    private void SpawnWarrior(GameObject warriorGo)
    {
        GameManager.instance.GetTownHallObject(team).resources.TakeFood(1);
        var warrior = Instantiate(warriorGo, warriorSpawnPos.transform.position, Quaternion.identity);
        Debug.Log("Spawned " + warrior.name + " at " + warrior.transform.position);
    }

    public void CreateScout()
    {
        if (!creatingWarrior && GameManager.instance.GetTownHallObject(team).resources.TakeFood(1))
        {
            // Mo�na tworzy� jednostkę
            // timer start
            warriorToCreate = warriorScoutPrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Scout creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
        }
    }

    public void CreateMelee()
    {
        if (!creatingWarrior && GameManager.instance.GetTownHallObject(team).resources.TakeFood(1))
        {
            // Mo�na tworzy� jednostkę
            // timer start
            warriorToCreate = warriorMeleePrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Mele warrior creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
        }
    }

    public void CreateRanged()
    {
        if (!creatingWarrior && GameManager.instance.GetTownHallObject(team).resources.TakeFood(1))
        {
            // Mo�na tworzy� jednostkę
            // timer start
            warriorToCreate = warriorRangedPrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Archer creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
        }
    }

    private void OnSelect()
    {
        
        GameManager.instance.UIManager.ShowUnitMenu(this);
    }

    private void OnDeselect()
    {
        GameManager.instance.UIManager.HideUnitMenu();
    }

    // public void DestroyUnitMenu()
    // {
    //     if (unitMenu)
    //         Destroy(unitMenu);
    // }

    public override void EventManager_OnBuildingFinished(Structure str)
    {

    }

    public override void EventManager_OnBuildingDestroyed()
    {

    }

}
