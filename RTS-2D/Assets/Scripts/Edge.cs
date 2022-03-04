using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Edge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 cameraMoveDelta;
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateCameraDelta(cameraMoveDelta);
        //Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateCameraDelta(-cameraMoveDelta);
        //Debug.Log("Mouse Exit");
    }

    private void UpdateCameraDelta(Vector2 value)
    {
        EventManager.EdgeScroll(value);
    }
}
