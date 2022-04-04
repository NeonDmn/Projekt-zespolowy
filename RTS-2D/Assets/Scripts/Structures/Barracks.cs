using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Barracks : Structure
{
    public GameObject barracksPrefab;
    public Transform barracksSpawnPos;
    private float barracksCreationTime;
    private bool barracksTimerRunning = false;
    private bool isPlacingBarracks = false;
    Vector2 barracksPosition;
    private Camera cam;

    private void Start()
    {
        OnBuildingFinished();
        cam = Camera.main;

    }
    private void Update()
    {
        //timer 
        if (barracksTimerRunning)
        {
            if (barracksCreationTime > 0)
            {
                barracksCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnBarracks();
                barracksTimerRunning = false;
            }
        }

        if (isPlacingBarracks && Mouse.current.leftButton.wasPressedThisFrame)
        {
            barracksPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            CreateBarracks();
            isPlacingBarracks = false;
        }
    }

    public void selectBarracksPosition()
    {
        GameObject.Find("Barracks").GetComponent<Barracks>().isPlacingBarracks = true;
    }


    private void CreateBarracks()
    {
        if (!ResourceManager.Instance.TakeWood(300) || !ResourceManager.Instance.TakeMetal(150))
        {
            print("Nie mo¿na utworzyæ koszar.");
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            barracksCreationTime = 20f;
            barracksTimerRunning = true;
        }
    }

    private void SpawnBarracks()
    {
        Instantiate(barracksPrefab, barracksPosition, Quaternion.identity);
    }


    public override void OnBuildingFinished()
    {

    }

    public override void OnBuildingDestroyed()
    {

    }
}
