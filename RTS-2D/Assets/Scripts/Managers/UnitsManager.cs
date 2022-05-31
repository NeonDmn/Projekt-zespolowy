using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager
{
    // Start is called before the first frame update
    //Listy wszystkich dostępnych jednostek 
    List<Worker> workes = new List<Worker>();

    //po dodaniu jednostek jak łucznik zwiadowca itp należało by zmodyfikować klasę od dodatkowe listy 


    public void AddWorker(Worker worker)
    {
        workes.Add(worker);
        worker.GetComponent<ObjectHealth>().onObjectDie += RemoveWorker;
        Debug.Log("Worker was added!!!");
    }

    private void RemoveWorker(ObjectHealth workerHealth)
    {
        workerHealth.onObjectDie -= RemoveWorker;
        workes.Remove(workerHealth.GetComponent<Worker>());
    }

    public List<Worker> GetWorkers()
    {
        return workes;
    }
}
