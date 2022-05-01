using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : Structure
{
    public GameObject workerPrefab;
    public Transform workerSpawnPos;
    private float workerCreationTime;

    private bool timerRunning = false;
    public void CreateWorker()
    {
        if (!ResourceManager.Instance.TakeFood(1))
        {
            // Nie można utworzyć workera
            // error
        }
        else
        {
            // Można tworzyć
            // timer start
            workerCreationTime = 3f;
            timerRunning = true;
        }
    }

    private void SpawnWorker()
    {
        GameObject obj = Instantiate(workerPrefab, workerSpawnPos.position, Quaternion.identity);
        UnitsManager.Instance.AddWorker(obj.GetComponent<Worker>());
    }

    private void Update()
    {
        //timer 
        if (timerRunning)
            if (workerCreationTime > 0)
                workerCreationTime -= Time.deltaTime;
            else
            {
                SpawnWorker();
                timerRunning = false;
            }

    }
}
