using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenEdge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 cameraMoveDelta;
    [SerializeField] bool show = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Camera moving on screen ledge: " + name);
        UpdateCameraDelta(cameraMoveDelta);
        //
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

    private void OnValidate()
    {
        if (show)
        {
            Image img = GetComponent<Image>();
            img.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            Image img = GetComponent<Image>();
            img.color = new Color(0, 0, 0, 0);
        }
    }
}
