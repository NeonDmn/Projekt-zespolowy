using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    private static Notification notification;
    public static GameManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(_instance);
        }

        notification = GetComponent<Notification>();
        _instance = this;
    }
    #endregion


    GameObject townHall;
    private void Start()
    {
        townHall = GameObject.FindGameObjectWithTag("TownHall");
    }
    public GameObject GetTownHallObject(int team)
    {
        return townHall;
    }
    public void sendNotification(string text){
        notification.notify(text);
    }
}
