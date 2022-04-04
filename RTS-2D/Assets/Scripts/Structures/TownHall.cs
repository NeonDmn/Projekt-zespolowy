using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TownHall : Structure
{
    public GameObject workerPrefab;
    public Transform workerSpawnPos;
    private float workerCreationTime;
    private bool workerTimerRunning = false;

    public GameObject barracksPrefab;
    public Transform barracksSpawnPos;
    private float barracksCreationTime;
    private bool barracksTimerRunning = false;
    private bool isPlacingBarracks = false;
    Vector2 barracksPosition;
	
	public GameObject farmPrefab;
    public Transform farmSpawnPos;
    private float farmCreationTime;
    private bool farmTimerRunning = false;
    private bool isPlacingFarm = false;
    Vector2 farmPosition;
	
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
	}
	
    public void CreateWorker()
    {
        if (!ResourceManager.Instance.TakeFood(1)) {
            // Nie można utworzyć workera
            // error
        }
        else {
            // Można tworzyć
            // timer start
            workerCreationTime = 3f;
            workerTimerRunning = true;
        }
    }

    private void SpawnWorker() {
        Instantiate(workerPrefab, workerSpawnPos.position, Quaternion.identity);
    }

    public void selectBarracksPosition()
    {
        GameObject.Find("TownHall").GetComponent<TownHall>().isPlacingBarracks = true;
    }


    private void CreateBarracks()
    {
        if (!ResourceManager.Instance.TakeFood(300) || !ResourceManager.Instance.TakeMetal(150)) {
            print("Nie można utworzyć koszar.");
        }
        else {
            // Można tworzyć
            // timer start
            barracksCreationTime = 20f;
            barracksTimerRunning = true;
        }
    }

    private void SpawnBarracks() {
        Instantiate(barracksPrefab, barracksPosition, Quaternion.identity);
    }
	
	public void selectFarmPosition()
    {
        GameObject.Find("TownHall").GetComponent<TownHall>().isPlacingFarm = true;
    }


    private void CreateFarm()
    {
        if (!ResourceManager.Instance.TakeFood(100) || !ResourceManager.Instance.TakeMetal(100)) {
            print("Nie można utworzyć farmy.");
        }
        else {
            // Można tworzyć
            // timer start
            farmCreationTime = 20f;
            farmTimerRunning = true;
        }
    }

    private void SpawnFarm() {
        Instantiate(farmPrefab, farmPosition, Quaternion.identity);
    }

	public void selectStoragePosition()
    {
        GameObject.Find("TownHall").GetComponent<TownHall>().isPlacingStorage = true;
    }


    private void CreateStorage()
    {
        if (!ResourceManager.Instance.TakeFood(150) || !ResourceManager.Instance.TakeMetal(50)) {
            print("Nie można utworzyć magazynu.");
        }
        else {
            // Można tworzyć
            // timer start
            storageCreationTime = 15f;
            storageTimerRunning = true;
        }
    }

    private void SpawnStorage() {
        Instantiate(storagePrefab, storagePosition, Quaternion.identity);
    }

    private void Update() {
        //timer 
        if (workerTimerRunning)
        {
            if (workerCreationTime > 0)
            {
                workerCreationTime -= Time.deltaTime;
            }
            else
            {
                SpawnWorker();
                workerTimerRunning = false;
            } 
        }

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
}
