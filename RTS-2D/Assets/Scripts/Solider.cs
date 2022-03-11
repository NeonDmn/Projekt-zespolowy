using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : MonoBehaviour
{

    private SpriteRenderer sprRenderer;

    private bool soliderSelected;

    public static bool dragSelectedSolidersAllowed, mouseOverSolider;

    private Vector2 mousePos;

    private float dragOffsetX, dragOffsetY;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        soliderSelected = false;
        dragSelectedSolidersAllowed = false;
        mouseOverSolider = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseInputs>())
        {
            sprRenderer.color = new Color(1f, 0f, 0f, 1f);
            soliderSelected = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseInputs>() && Input.GetMouseButton(0))
        {
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            soliderSelected = false;
        }
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }

        if(Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(soliderSelected && dragSelectedSolidersAllowed)
        {
            transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
        }

        if(Input.GetMouseButtonDown(1))
        {
            soliderSelected = false;
            dragSelectedSolidersAllowed = false;
            sprRenderer.color = new Color(1f,1f,1f,1f);
        }
    }

    private void OnMouseDown()
    {
        mouseOverSolider = true;
    }

    private void OnMouseUp()
    {
        mouseOverSolider = false;
        dragSelectedSolidersAllowed = false;
    }

    private void OnMouseDrag()
    {
        dragSelectedSolidersAllowed = true;

        if(!soliderSelected)
        {
            dragSelectedSolidersAllowed = false;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x - dragOffsetX , mousePos.y - dragOffsetY);
    }
}
