using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMenu : MonoBehaviour
{
    private Barracks barracks;

    [SerializeField] Image unitImage;
    [SerializeField] Slider unitTimeSlider;
    [SerializeField] Sprite noUnitSprite;

    public void Init(Barracks barracks)
    {
        this.barracks = barracks;
        if (barracks.creatingWarrior)
        {
            After();
        }
        else
        {
            ResetMenu();
        }
        barracks.OnUnitSpawned += ResetMenu;
    }

    private void Update() {
        if (!barracks || barracks.warriorCreationTime <= 0.0f) return;
        unitTimeSlider.value = (unitTimeSlider.maxValue - barracks.warriorCreationTime);
    }

    public void SpawnWarrior()
    {
        barracks.CreateMelee();
        After();
        
    }

    public void SpawnArcher()
    {
        barracks.CreateRanged();
        After();
    }

    public void SpawnScout()
    {
        barracks.CreateScout();
        After();
    }

    void After()
    {
        // Ustaw obrazek jednostki w oknie
        Sprite unitSprite = barracks.warriorToCreate.GetComponent<SpriteRenderer>().sprite;
        unitImage.sprite = unitSprite;

        // maksymalna wartość slidera
        unitTimeSlider.maxValue = 3.0f;
    }

    public void ResetMenu()
    {
        unitTimeSlider.value = 0f;
        unitImage.sprite = noUnitSprite;
    }
}
