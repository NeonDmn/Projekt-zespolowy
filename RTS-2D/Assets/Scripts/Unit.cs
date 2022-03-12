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

    /*
     * Wykonywana gdy jednostka jest zaznaczana przez skrypt Selection
     */
    private void OnSelect()
    {
        sprRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    /*
     * Wykonywana gdy jednostka jest odznaczana przez skrypt Selection
     */
    private void OnDeselect()
    {
        sprRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
