using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildWidget : MonoBehaviour
{
    [SerializeField] LayerMask buildingDenyMask;

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
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            spriteRenderer.color = new Color(1, 0, 0, 1);
        }
    }

    public bool CanBuild()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.zero, 0.0f, buildingDenyMask);
        return !hit;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
