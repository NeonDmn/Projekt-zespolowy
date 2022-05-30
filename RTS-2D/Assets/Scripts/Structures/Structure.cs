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

    [SerializeField] Sprite buildSprite;
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
        audioManager.setBuild(audioClip[0]);
        audioManager.getBuild().Play();
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
                EventManager.OnBuildingFinished?.Invoke(this);
                GetComponent<SpriteRenderer>().sprite = structureSprite;
                
                buildingFinished = true;
                audioManager.setDeath(audioClip[1]);
            }
        }
            
    }

    public virtual void EventManager_OnBuildingFinished(Structure str) {}
    public virtual void EventManager_OnBuildingDestroyed() {}

}
