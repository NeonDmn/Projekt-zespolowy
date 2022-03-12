using UnityEngine;

public class Unit : MonoBehaviour
{

    private SpriteRenderer sprRenderer;

    void Start()
    {
        Selection.Instance.unitList.Add(this.gameObject);

        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        Selection.Instance.unitList.Add(this.gameObject);
    }

    private void OnSelect()
    {
        sprRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    private void OnDeselect()
    {
        sprRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
