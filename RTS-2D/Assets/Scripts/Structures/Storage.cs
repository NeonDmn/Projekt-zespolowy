using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Storage : Structure
{
    public GameObject storagePrefab;
    public Transform storageSpawnPos;
    private float storageCreationTime;
    private bool storageTimerRunning = false;
    private bool isPlacingStorage = false;
    Vector2 storagePosition;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        //OnBuildingFinished();
    }

    private void Update()
    {
        if (storageTimerRunning)
        {
            if (storageCreationTime > 0)
            {
                storageCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnStorage();
                OnBuildingFinished();
                storageTimerRunning = false;
            }
        }

        if (isPlacingStorage && Mouse.current.rightButton.wasPressedThisFrame)
        {
            storagePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //CreateStorage();
            isPlacingStorage = false;
        }
    }

    public void selectStoragePosition()
    {
        GameObject.Find("Storage").GetComponent<Storage>().isPlacingStorage = true;
    }


    // private void CreateStorage()
    // {
    //     if (MouseInputs.collide == true)
    //     {
    //         if (ResourceManager.Instance.TakeWood(woodCost) || ResourceManager.Instance.TakeMetal(metalCost))
    //         {
    //             Debug.Log("Za ma�o surowc�w by wybudowa� magazyn");
    //         }
    //         else
    //         {
    //             Debug.Log("Budowa magazynu rozpocz�ta");
    //             // Mo�na tworzy�
    //             // timer start
    //             storageCreationTime = 15f;
    //             storageTimerRunning = true;
    //         }
    //     }
    // }

    private void SpawnStorage()
    {
        Instantiate(storagePrefab, storagePosition, Quaternion.identity);
    }

    public override void OnBuildingFinished()
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxResource(Resource.Type.CRYSTAL, 20);
        th.resources.AddToMaxResource(Resource.Type.WOOD, 75);
        th.resources.AddToMaxResource(Resource.Type.METAL, 50);
    }

    public override void OnBuildingDestroyed()
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxResource(Resource.Type.CRYSTAL, -20);
        th.resources.AddToMaxResource(Resource.Type.WOOD, -75);
        th.resources.AddToMaxResource(Resource.Type.METAL, -50);
    }
}
