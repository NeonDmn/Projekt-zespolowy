using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float edgeScrollSpeed = 1f;
    [SerializeField] Camera controlledCamera;
    [SerializeField] private BoxCollider2D boundingBox;
    [SerializeField] private Transform cameraMoveTransform;

    private Vector2 camDeltaFromInput;
    private Vector2 camDeltaFromEdge;
    private float camHalfHeight;
    private float camHalfWidth;

    private void Awake()
    {
        if (!controlledCamera)
            controlledCamera = Camera.main;
        CalculateCameraWorldDimentions();

        EventManager.EdgeScroll += EventManager_UpdateEgdeScroll;
    }

    void LateUpdate()
    {
        MoveCamera();
    }

    /* 
     * Metoda wysoływana w momencie wykrycia nacisnięcia kalwisza
     */
    public void OnCameraMove(InputAction.CallbackContext ctx)
    {
        camDeltaFromInput = ctx.ReadValue<Vector2>();
    }

    private void MoveCamera()
    {
        // Obecna pozycja kamery
        Vector2 newCamPos = cameraMoveTransform.position;
        Vector2 camPosDelta = ( (camDeltaFromInput * speed) + (camDeltaFromEdge * edgeScrollSpeed) ) * Time.deltaTime;

        Debug.Log(camPosDelta.x + " " + camPosDelta.y);

        // Pozycja kamery po dodaniu różnicy na osi X
        newCamPos.x += camPosDelta.x;
        if (!IsInBounds(newCamPos))
            newCamPos.x = cameraMoveTransform.position.x;

        // Pozycja kamery po dodaniu różnicy na osi Y
        newCamPos.y += camPosDelta.y;
        if (!IsInBounds(newCamPos))
            newCamPos.y = cameraMoveTransform.position.y;

        // Ostateczna pozycja kamery
        cameraMoveTransform.position = newCamPos;
    }

    /* 
     * Metoda obliczająca, czy podany punkt znajduje się w granicach obiektu boundingBox
     */
    private bool IsInBounds(Vector2 position)
    {
        Vector2 topRight = new Vector2(position.x + camHalfWidth, position.y + camHalfHeight);
        Vector2 botLeft = new Vector2(position.x - camHalfWidth, position.y - camHalfHeight);

        if (boundingBox.OverlapPoint(topRight) && boundingBox.OverlapPoint(botLeft))
            return true;
        return false;
    }

    /* 
     * Oblicza wysokość i szerokość obrazu kamery w jednostkach Unity
     */
    private void CalculateCameraWorldDimentions()
    {
        camHalfHeight = controlledCamera.orthographicSize;
        camHalfWidth = controlledCamera.aspect * camHalfHeight;
    }
    private void EventManager_UpdateEgdeScroll(Vector2 value)
    {
        camDeltaFromEdge += value;
    }
}
