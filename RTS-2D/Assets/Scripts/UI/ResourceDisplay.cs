using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] Text currentText;
    [SerializeField] Text maxText;

    public void UpdateUI(int current, int max)
    {
        currentText.text = current.ToString();
        maxText.text = max.ToString();
    }
}
