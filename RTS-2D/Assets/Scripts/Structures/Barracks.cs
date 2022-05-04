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
    public GameObject warriorMeleePrefab;
    public GameObject warriorScoutPrefab;
    public GameObject warriorRangedPrefab;
    public Transform warriorSpawnPos;
    private float warriorMeleeCreationTime;
    private bool warriorMeleeTimerRunning = false;
    private float warriorScoutCreationTime;
    private bool warriorScoutTimerRunning = false;
    private float warriorRangedCreationTime;
    private bool warriorRangedTimerRunning = false;
    private bool canCreate = false;

    private void Start()
    {
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
            CreateBarracks();
            isPlacingBarracks = false;
        }

        if (warriorMeleeTimerRunning)
        {
            if (warriorMeleeCreationTime > 0)
            {
                warriorMeleeCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnMelee();
                warriorMeleeTimerRunning = false;
            }
        }

        if (warriorScoutTimerRunning)
        {
            if (warriorScoutCreationTime > 0)
            {
                warriorScoutCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnScout();
                warriorScoutTimerRunning = false;
            }
        }

        if (warriorRangedTimerRunning)
        {
            if (warriorRangedCreationTime > 0)
            {
                warriorRangedCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnRanged();
                warriorRangedTimerRunning = false;
            }
        }
    }

    public void selectBarracksPosition()
    {
        GameObject.Find("Barracks").GetComponent<Barracks>().isPlacingBarracks = true;
    }


    private void CreateBarracks()
    {
        if (MouseInputs.collide == true)
        {
            if (ResourceManager.Instance.TakeWood(woodCost) || ResourceManager.Instance.TakeMetal(metalCost)) //zmieniæ warunek
            {
                Debug.Log("Za ma³o surowców by wybudowaæ koszary");
            }
            else
            {
                Debug.Log("Budowa koszar rozpoczêta");
                // Mo¿na tworzyæ
                // timer start
                barracksCreationTime = 5f;//zmieniæ czas
                barracksTimerRunning = true;
            }
        }
    }

    private void SpawnBarracks()
    {
        Instantiate(barracksPrefab, barracksPosition, Quaternion.identity);
    }


    public void CreateScout()
    {
        if (!ResourceManager.Instance.TakeFood(1) && !canCreate) //zmieniæ jedzenie
        {
            // Nie mo¿na utworzyæ workera
            // error
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            warriorScoutCreationTime = 3f; //zmieniæ czas
            warriorScoutTimerRunning = true;
        }
    }

    public void CreateMelee()
    {
        if (!ResourceManager.Instance.TakeFood(1) && !canCreate) //zmieniæ jedzenie
        {
            // Nie mo¿na utworzyæ workera
            // error
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            warriorMeleeCreationTime = 3f; //zmieniæ czas
            warriorMeleeTimerRunning = true;
        }
    }

    public void CreateRanged()
    {
        if (!ResourceManager.Instance.TakeFood(1) && !canCreate) //zmieniæ jedzenie
        {
            // Nie mo¿na utworzyæ workera
            // error
        }
        else
        {
            // Mo¿na tworzyæ
            // timer start
            warriorRangedCreationTime = 3f; //zmieniæ czas
            warriorRangedTimerRunning = true;
        }
    }

    private void SpawnScout()
    {
        Instantiate(warriorScoutPrefab, warriorSpawnPos.position, Quaternion.identity);
    }

    private void SpawnMelee()
    {
        Instantiate(warriorMeleePrefab, warriorSpawnPos.position, Quaternion.identity);
    }


    private void SpawnRanged()
    {
        Instantiate(warriorRangedPrefab, warriorSpawnPos.position, Quaternion.identity);
    }

    public override void OnBuildingFinished()
    {
        canCreate = true;
    }

    public override void OnBuildingDestroyed()
    {
        canCreate = false;
    }

}
