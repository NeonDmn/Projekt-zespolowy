using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    // List<Notification> notifications;
    [SerializeField] GameObject notificationMenu;

    public void ToggleNotificationMenu() {
        notificationMenu.SetActive(!notificationMenu.activeInHierarchy);
    }

    //public void AddNotification()
    //public Notification CreateNotification()
}
