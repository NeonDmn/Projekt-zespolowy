using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour
{

    public GameObject[] unitList;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake(){
        unitList = GameObject.FindGameObjectsWithTag("Unit");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject Unit in unitList){
            if(Vector3.Distance(Unit.transform.position,transform.position) < 5){
                transform.position = Vector3.MoveTowards(transform.position,Unit.transform.position,Time.deltaTime);
                break;
            } 
        }
    }
}
