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
        //Debug.Log("Pojawił się budynek " + name + " frakcji " + GetComponent<PlayerTeam>().team + " na " + transform.position);
        //OnBuildingFinished();
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
                OnBuildingFinished();
                barracksTimerRunning = false;
            }
        }

        if (isPlacingBarracks && Mouse.current.rightButton.wasPressedThisFrame)
        {
            barracksPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            //CreateBarracks();
            isPlacingBarracks = false;
        }
    }

    public void selectBarracksPosition()
    {
        GameObject.Find("Barracks").GetComponent<Barracks>().isPlacingBarracks = true;
    }


    // private void CreateBarracks()
    // {
    //     if (MouseInputs.collide == true)
    //     {
    //         if (ResourceManager.Instance.TakeWood(woodCost) || ResourceManager.Instance.TakeMetal(metalCost))
    //         {
    //             Debug.Log("Za ma�o surowc�w by wybudowa� koszary");
    //         }
    //         else
    //         {
    //             Debug.Log("Budowa koszar rozpocz�ta");
    //             // Mo�na tworzy�
    //             // timer start
    //             barracksCreationTime = 20f;
    //             barracksTimerRunning = true;
    //         }
    //     }
    // }

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
