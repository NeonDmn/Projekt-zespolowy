using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] Camera controlledCamera;
    [SerializeField] private BoxCollider2D boundingBox;
    [SerializeField] private Transform cameraMoveTransform;

    private Vector2 cameraPosDelta;
    private Vector2 edgeScrollVector;
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

    /* Metoda wysoływana asynchronicznie w momencie wykrycia nacisnięcia kalwisza  */
    public void OnCameraMove(InputAction.CallbackContext ctx)
    {
        Vector2 moveVec = ctx.ReadValue<Vector2>();
        cameraPosDelta = moveVec * speed * 0.1f;
    }

    private void MoveCamera()
    {
        // Obecna pozycja kamery
        Vector2 newCamPos = cameraMoveTransform.position;

        // Pozycja kamery po dodaniu różnicy na osi X
        newCamPos.x += cameraPosDelta.x + edgeScrollVector.x;
        if (!IsInBounds(newCamPos))
            newCamPos.x = cameraMoveTransform.position.x;

        // Pozycja kamery po dodaniu różnicy na osi Y
        newCamPos.y += cameraPosDelta.y + edgeScrollVector.y;
        if (!IsInBounds(newCamPos))
            newCamPos.y = cameraMoveTransform.position.y;

        // Ostateczna pozycja kamery
        cameraMoveTransform.position = newCamPos;
    }

    /* Metoda obliczająca, czy podany punkt znajduje się w granicach obiektu BoudingBox2D */
    private bool IsInBounds(Vector2 position)
    {
        Vector2 topRight = new Vector2(position.x + camHalfWidth, position.y + camHalfHeight);
        Vector2 botLeft = new Vector2(position.x - camHalfWidth, position.y - camHalfHeight);

        if (boundingBox.OverlapPoint(topRight) && boundingBox.OverlapPoint(botLeft))
            return true;
        return false;
    }

    /* Oblicza wysokość i szerokość obrazu kamery w jednostkach Unity */
    private void CalculateCameraWorldDimentions()
    {
        camHalfHeight = controlledCamera.orthographicSize;
        camHalfWidth = controlledCamera.aspect * camHalfHeight;
    }
    private void EventManager_UpdateEgdeScroll(Vector2 value)
    {
        edgeScrollVector += value;
    }
}
