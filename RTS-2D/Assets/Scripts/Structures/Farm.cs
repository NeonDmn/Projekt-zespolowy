using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Farm : Structure
{
    public GameObject farmPrefab;
    public Transform farmSpawnPos;
    private float farmCreationTime;
    private bool farmTimerRunning = false;
    private bool isPlacingFarm = false;
    Vector2 farmPosition;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        OnBuildingFinished();
    }

    private void Update()
    {
        //timer 
        if (farmTimerRunning)
        {
            if (farmCreationTime > 0)
            {
                farmCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnFarm();
                farmTimerRunning = false;
            }
        }

        if (isPlacingFarm && Mouse.current.leftButton.wasPressedThisFrame)
        {
            farmPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            CreateFarm();
            isPlacingFarm = false;
        }
    }
    public void selectFarmPosition()
    {
        GameObject.Find("Farm").GetComponent<Farm>().isPlacingFarm = true;
    }


    private void CreateFarm()
    {
        if (ResourceManager.Instance.TakeWood(100) || ResourceManager.Instance.TakeMetal(100))
        {
            print("Nie mo¿na utworzyæ farmy.");
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            farmCreationTime = 20f;
            farmTimerRunning = true;
        }
    }

    private void SpawnFarm()
    {
        Instantiate(farmPrefab, farmPosition, Quaternion.identity);
    }

    public override void OnBuildingFinished()
    {
        ResourceManager.Instance.AddToMaxFood(50);
    }

    public override void OnBuildingDestroyed()
    {
        ResourceManager.Instance.AddToMaxFood(-50);
    }
}
