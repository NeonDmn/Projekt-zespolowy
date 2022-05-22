using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // List<Notification> notifications;
    [SerializeField] Canvas UICanvas;
    [SerializeField] GameObject notificationMenu;
    [SerializeField] GameObject buildMenu;
    [SerializeField] GameObject unitMakeMenu;

    public void ToggleNotificationMenu() {
        notificationMenu.SetActive(!notificationMenu.activeInHierarchy);
    }

    public GameObject AddToCanvas(GameObject gameObject, Vector2 position)
    {
        var obj = Instantiate(gameObject, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
        obj.transform.SetParent(UICanvas.transform);

        return obj;
    }

    public void ShowBuildMenu()
    {
        buildMenu.SetActive(true);
    }

    public void HideBuildMenu()
    {
        buildMenu.SetActive(false);
    }

    public void ShowUnitMenu(Barracks initBarracks)
    {
        unitMakeMenu.GetComponent<UnitMenu>().Init(initBarracks);
        unitMakeMenu.SetActive(true);
    }

    public void HideUnitMenu()
    {
        unitMakeMenu.SetActive(false);
    }

    //public void AddNotification()
    //public Notification CreateNotification()
}
