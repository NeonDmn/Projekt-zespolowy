using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Barracks : Structure
{

    [SerializeField] GameObject barracksNotificationFinishedPrefab;

    public GameObject warriorMeleePrefab;
    public GameObject warriorScoutPrefab;
    public GameObject warriorRangedPrefab;
    public Transform warriorSpawnPos;

    public UnityAction OnUnitSpawned;

    public float warriorCreationTime = 0.0f;
    public bool creatingWarrior { get; private set; }
    public GameObject warriorToCreate { get; private set; }

    PlayerTeam.Team team;
    ResourceManager resourceManager;

    public override void Start() {
        base.Start();

        team = GetComponent<PlayerTeam>().team;
        resourceManager = GameManager.instance.GetTownHallObject(team).resources;
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
        //GameManager.instance.GetTownHallObject(team).resources.TakeFood(1);
        var warrior = Instantiate(warriorGo, warriorSpawnPos.transform.position, Quaternion.identity);
        OnUnitSpawned?.Invoke();

        Debug.Log("Spawned " + warrior.name + " at " + warrior.transform.position);
    }

    public void CreateScout()
    {
        resourceManager.AddToCart(Resource.Type.CRYSTAL, 5);
        if (!creatingWarrior && resourceManager.GetFreeFood() >= 3 && resourceManager.FinalizeTransaction())
        {
            // Mo�na tworzy� jednostkę
            // timer start
            resourceManager.TakeFood(3);
            warriorToCreate = warriorScoutPrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Scout creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
            resourceManager.ClearCart();
        }
    }

    public void CreateMelee()
    {
        resourceManager.AddToCart(Resource.Type.CRYSTAL, 8);
        if (!creatingWarrior && resourceManager.GetFreeFood() >= 5 && resourceManager.FinalizeTransaction())
        {
            // Mo�na tworzy� jednostkę
            // timer start
            resourceManager.TakeFood(5);
            warriorToCreate = warriorMeleePrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Mele warrior creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
            resourceManager.ClearCart();
        }
    }

    public void CreateRanged()
    {
        resourceManager.AddToCart(Resource.Type.CRYSTAL, 10);
        if (!creatingWarrior && resourceManager.GetFreeFood() >= 2 && resourceManager.FinalizeTransaction())
        {
            // Mo�na tworzy� jednostkę
            // timer start
            resourceManager.TakeFood(2);
            warriorToCreate = warriorRangedPrefab;
            warriorCreationTime = 3.0f;
            creatingWarrior = true;
            Debug.Log("Archer creation started!");
        }
        else
        {
            // Nie mo�na utworzy� jednostkę
            // error
            resourceManager.ClearCart();
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
        if(buildingFinished){
        if(GetComponent<PlayerTeam>().team==PlayerTeam.Team.Friendly && !onlyOnce){
           onlyOnce = true;
           var temp =  Instantiate(barracksNotificationFinishedPrefab, new Vector3(transform.position.x, (transform.position.y+1.0f), transform.position.z), Quaternion.identity);
           Destroy(temp,3.0f);
        }
        }
    }

    public override void EventManager_OnBuildingDestroyed(ObjectHealth oh)
    {

    }

}
