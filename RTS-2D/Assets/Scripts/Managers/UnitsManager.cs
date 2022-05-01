using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static UnitsManager _instance;
    public static UnitsManager Instance { get { return _instance; } }
    //Listy wszystkich dostępnych jednostek 
    List<Worker> workes = new List<Worker>();

    //po dodaniu jednostek jak łucznik zwiadowca itp należało by zmodyfikować klasę od dodatkowe listy 

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void AddWorker(Worker worker)
    {
        workes.Add(worker);
        Debug.Log("Worker was added!!!");
    }
    public List<Worker> GetWorkers()
    {
        return workes;
    }
}
