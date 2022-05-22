using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float buildTime;
    public int life;
    public int woodCost;
    public int metalCost;

    protected bool buildingFinished;

    [SerializeField] Sprite buildSprite;
    Sprite structureSprite;

    public virtual void Start()
    {
        Debug.Log("Pojawił się budynek " + name + " frakcji " + GetComponent<PlayerTeam>().team + " na " + transform.position);

        buildingFinished = false;

        EventManager.OnBuildingFinished += EventManager_OnBuildingFinished;

        structureSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = buildSprite;
    }

    public virtual void Update() {
        if (!buildingFinished)
        {
            if (buildTime > 0.0f)
                buildTime -= Time.deltaTime;
            else
            {
                EventManager.OnBuildingFinished?.Invoke(this);
                GetComponent<SpriteRenderer>().sprite = structureSprite;
                buildingFinished = true;
            }
        }
            
    }

    public virtual void EventManager_OnBuildingFinished(Structure str) {}
    public virtual void EventManager_OnBuildingDestroyed() {}

}
