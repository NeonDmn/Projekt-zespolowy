using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float buildTime;
    public int life;
    public int woodCost;
    public int metalCost;

    private void Start()
    {
        Debug.Log("Pojawił się budynek " + name + " frakcji " + GetComponent<PlayerTeam>().team + " na " + transform.position);
    }

    public virtual void OnBuildingFinished()
    {

    }

    public virtual void OnBuildingDestroyed()
    {

    }

}
