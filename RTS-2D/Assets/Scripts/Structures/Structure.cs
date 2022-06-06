using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    public float buildTime;
    [SerializeField]
    protected float life;
    public int woodCost;
    public int metalCost;

    public AudioClip[] audioClip;

    protected bool buildingFinished;
    protected bool onlyOnce;

    [SerializeField] protected Sprite buildSprite;
    Sprite structureSprite;

    public AudioManager audioManager;

    public virtual void Start()
    {
        Debug.Log("Pojawił się budynek " + name + " frakcji " + GetComponent<PlayerTeam>().team + " na " + transform.position);

        buildingFinished = false;

        EventManager.OnBuildingFinished += EventManager_OnBuildingFinished;

        structureSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = buildSprite;
        GetComponent<ObjectHealth>().setMaxHealth(life);
        GetComponent<ObjectHealth>().onObjectDie += ObjectHealth_Handle_OnObjectDie;
        GetComponent<ObjectHealth>().onObjectDie += EventManager_OnBuildingDestroyed;
        audioManager.setBuild(audioClip[0]);
        audioManager.getBuild().Play();
        onlyOnce =false;
    }

    public virtual void Update()
    {
        if (!buildingFinished)
        {
            

            if (buildTime > 0.0f)
            {
                buildTime -= Time.deltaTime;
                
            }
            else
            {
                
                buildingFinished = true;
                
                GetComponent<SpriteRenderer>().sprite = structureSprite;
                EventManager.OnBuildingFinished?.Invoke(this);
                
                audioManager.setDeath(audioClip[1]);
            }
        }
            
    }

    private void ObjectHealth_Handle_OnObjectDie(ObjectHealth oh)
    {
        oh.onObjectDie -= ObjectHealth_Handle_OnObjectDie;
        EventManager.OnBuildingDestroyed?.Invoke(this);
    }

    public virtual void EventManager_OnBuildingFinished(Structure str) {}
    public virtual void EventManager_OnBuildingDestroyed(ObjectHealth oh) {}

}
