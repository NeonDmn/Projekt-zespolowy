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
        //OnBuildingFinished();
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
                OnBuildingFinished();
                farmTimerRunning = false;
            }
        }

        if (isPlacingFarm && Mouse.current.rightButton.wasPressedThisFrame)
        {
            farmPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //CreateFarm();
            isPlacingFarm = false;
        }


    }
    public void selectFarmPosition()
    {
        GameObject.Find("Farm").GetComponent<Farm>().isPlacingFarm = true;
    }


    // private void CreateFarm()
    // {
    //     if (MouseInputs.collide == true)
    //     {
    //         if (ResourceManager.Instance.TakeWood(woodCost) || ResourceManager.Instance.TakeMetal(metalCost))
    //         {
    //             Debug.Log("Za ma�o surowc�w by wybudowa� farme.");
    //         }
    //         else
    //         {
    //             Debug.Log("Budowa farmy rozpocz�ta");
    //             // Mo�na tworzy�
    //             // timer start
    //             farmCreationTime = 20f;
    //             farmTimerRunning = true;
    //         }
    //     }
    // }

    private void SpawnFarm()
    {
        Instantiate(farmPrefab, farmPosition, Quaternion.identity);
    }

    public override void OnBuildingFinished()
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxFood(50);
    }

    public override void OnBuildingDestroyed()
    {
        TownHall th = GameManager.instance.GetTownHallObject(GetComponent<PlayerTeam>().team);

        th.resources.AddToMaxFood(-50);
    }

}
