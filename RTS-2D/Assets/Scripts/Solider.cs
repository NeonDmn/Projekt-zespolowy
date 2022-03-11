using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Solider : MonoBehaviour
{

    private SpriteRenderer sprRenderer;

    private bool soliderSelected;

    public static bool dragSelectedSolidersAllowed, mouseOverSolider;

    private Vector2 mousePos;

    private float dragOffsetX, dragOffsetY;

    private bool inputActionDown = false;
    private bool inputSelectDown = false;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        soliderSelected = false;
        dragSelectedSolidersAllowed = false;
        mouseOverSolider = false;
    }

    public void ActionInput(InputAction.CallbackContext ctx)
    {
        inputActionDown = ctx.ReadValueAsButton();

        if (inputActionDown)
        {
            soliderSelected = false;
            dragSelectedSolidersAllowed = false;
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void SelectInput(InputAction.CallbackContext ctx)
    {
        inputSelectDown = ctx.ReadValueAsButton();

        if (inputSelectDown)
        {
            dragOffsetX = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y - transform.position.y;
        }
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
        if (collision.gameObject.GetComponent<MouseInputs>() && inputSelectDown)
        {
            sprRenderer.color = new Color(1f, 1f, 1f, 1f);
            soliderSelected = false;
        }
    }


    void Update()
    {
        

        if(inputSelectDown)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if(soliderSelected && dragSelectedSolidersAllowed)
        {
            transform.position = new Vector2(mousePos.x - dragOffsetX, mousePos.y - dragOffsetY);
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

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector2(mousePos.x - dragOffsetX , mousePos.y - dragOffsetY);
    }
}
