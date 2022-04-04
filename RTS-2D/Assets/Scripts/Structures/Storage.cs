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
        OnBuildingFinished();
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
                storageTimerRunning = false;
            }
        }

        if (isPlacingStorage && Mouse.current.leftButton.wasPressedThisFrame)
        {
            storagePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            CreateStorage();
            isPlacingStorage = false;
        }
    }

    public void selectStoragePosition()
    {
        GameObject.Find("Storage").GetComponent<Storage>().isPlacingStorage = true;
    }


    private void CreateStorage()
    {
        if (!ResourceManager.Instance.TakeWood(150) || !ResourceManager.Instance.TakeMetal(50))
        {
            print("Nie mo¿na utworzyæ magazynu.");
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            storageCreationTime = 15f;
            storageTimerRunning = true;
        }
    }

    private void SpawnStorage()
    {
        Instantiate(storagePrefab, storagePosition, Quaternion.identity);
    }

    public override void OnBuildingFinished()
    {
        ResourceManager.Instance.AddToMaxResource(Resource.Type.CRYSTAL, 20);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.WOOD, 75);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.METAL, 50);
    }

    public override void OnBuildingDestroyed()
    {
        ResourceManager.Instance.AddToMaxResource(Resource.Type.CRYSTAL, -20);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.WOOD, -75);
        ResourceManager.Instance.AddToMaxResource(Resource.Type.METAL, -50);
    }
}
