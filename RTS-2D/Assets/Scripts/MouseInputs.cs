using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputs : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    private LineRenderer lineRenderer;
    private Vector2 initialMousePosition, currentMousePosition;
    private BoxCollider2D boxColl;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    void Update()
    {

        SelectBox();
        //RightButton();
        MiddleButton();
        
    }

    private void MiddleButton()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middle Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Input.mousePosition));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Input.mousePosition));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + " select");
            }
        }
    }

    private void RightButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Input.mousePosition));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Input.mousePosition));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(initialMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name + " select");
            }
        }
    }

    private void SelectBox()
    {
        if (Input.GetMouseButtonDown(0) && !Solider.mouseOverSolider)
        {
            lineRenderer.positionCount = 4;

            Debug.Log("Left Click");
            Debug.Log("Screen Point: " + mainCamera.WorldToScreenPoint(Input.mousePosition));
            Debug.Log("World Point: " + mainCamera.ScreenToWorldPoint(Input.mousePosition));
            initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            lineRenderer.SetPosition(0, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(1, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(2, new Vector2(initialMousePosition.x, initialMousePosition.y));
            lineRenderer.SetPosition(3, new Vector2(initialMousePosition.x, initialMousePosition.y));

            boxColl = gameObject.AddComponent<BoxCollider2D>();
            boxColl.isTrigger = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }

        if (Input.GetMouseButton(0) && !Solider.mouseOverSolider)
        {
            currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
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

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.positionCount = 0;
            Destroy(boxColl);
            transform.position = Vector3.zero;
        }
    }
}
