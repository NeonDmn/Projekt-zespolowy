using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildWidget : MonoBehaviour
{
    [SerializeField] LayerMask buildingDenyMask;
    //[SerializeField] LayerMask groundLayer;

    SpriteRenderer spriteRenderer;
    public GameObject buildingGO { get; private set; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }
    
    void Update()
    {
        transform.position = MouseInputs.GetMouseWorldPos();

        ValidPlacement(CanBuild());
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
            spriteRenderer.color = new Color(1, 1, 1, 0.9f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.9f);
        }
    }

    public bool CanBuild()
    {
        RaycastHit2D gHit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0.0f, 1 << LayerMask.NameToLayer("Ground"));
        if (!gHit)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.zero, 0.0f, buildingDenyMask);
        return !hit;
    }

    public static bool CanBuild(Vector2 position)
    {
        RaycastHit2D gHit = Physics2D.CircleCast(position, 0.1f, Vector2.zero, 0.0f, 1 << LayerMask.NameToLayer("Ground"));
        if (!gHit)
        {
            return false;
        }

        LayerMask noBuildingMask = LayerMask.GetMask("Impassable", "RoughTerrain", "Building", "Resource", "Unit");
        RaycastHit2D hit = Physics2D.CircleCast(position, 0.5f, Vector2.zero, 0.0f, noBuildingMask);
        return !hit;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
