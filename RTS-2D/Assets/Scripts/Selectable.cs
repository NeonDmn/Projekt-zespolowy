using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject selectedVisual;

    private void OnSelect()
    {
        selectedVisual.SetActive(true);
    }
    private void OnDeselect()
    {
        selectedVisual.SetActive(false);
    }
}
