using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    private LineRenderer lineRenderer;
    private Vector2 initialMousePosition, currentMousePosition;
    private BoxCollider2D boxColl;

    private bool inputActionDown = false;
    private bool inputSelectDown = false;

    private void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        SelectBox();
    }

    public void ActionInput(InputAction.CallbackContext ctx)
    {
        inputActionDown = ctx.ReadValueAsButton();

        if (inputActionDown)
        {
            Debug.Log("Right Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Mouse.current.position.ReadValue()));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + " select");
            }
        }
    }

    public void SelectInput(InputAction.CallbackContext ctx)
    {
        inputSelectDown = ctx.ReadValueAsButton();

        if (inputSelectDown && !Solider.mouseOverSolider)
        {
            lineRenderer.positionCount = 4;

            Debug.Log("Left Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Mouse.current.position.ReadValue()));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            lineRenderer.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(1, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(2, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(3, new Vector2(initialMousePosition.x, initialMousePosition.y));

            boxColl = gameObject.AddComponent<BoxCollider2D>();
            boxColl.isTrigger = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }

        if (!inputSelectDown)
        {
            lineRenderer.positionCount = 0;
            Destroy(boxColl);
            transform.position = Vector3.zero;
        }
    }

    public void ChecktInput(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            Debug.Log("Middle Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Mouse.current.position.ReadValue()));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + " select");
            }
        }
    }

    private void SelectBox()
    {
        if (inputSelectDown && !Solider.mouseOverSolider)
        {
            currentMousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            lineRenderer.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(1, new Vector2(initialMousePosition.x, currentMousePosition.y));
            lineRenderer.SetPosition(2, new Vector2(currentMousePosition.x, currentMousePosition.y));
            lineRenderer.SetPosition(3, new Vector2(currentMousePosition.x, initialMousePosition.y));

            transform.position = (currentMousePosition + initialMousePosition) / 2;

            boxColl.size = new Vector2(
                Mathf.Abs(initialMousePosition.x - currentMousePosition.x),
                Mathf.Abs(initialMousePosition.y - currentMousePosition.y)
                );
        }
    }
}
