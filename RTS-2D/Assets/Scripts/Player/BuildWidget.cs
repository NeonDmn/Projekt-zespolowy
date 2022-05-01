using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildWidget : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject buildingGO { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void SetBuilding(GameObject buildingGO)
    {
        this.buildingGO = buildingGO;
        SpriteRenderer sprR = buildingGO.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprR.sprite;
    }

    public void ValidPlacement(bool canPlace)
    {
        if (canPlace)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.3f);
        }
    }
}
