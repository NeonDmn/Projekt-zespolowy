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

    [Space]

    [SerializeField] ResourceDisplay uiDisplayWood;
    [SerializeField] ResourceDisplay uiDisplayMetal;
    [SerializeField] ResourceDisplay uiDisplayCrystal;
    [SerializeField] ResourceDisplay uiDisplayFood;

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

    public void UpdateUIResourceDisplay()
    {
        TownHall th = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Friendly);

        // Pobierz maksymalne wartości surowców
        int maxWood = th.resources.getMaxResource(Resource.Type.WOOD);
        int maxMetal = th.resources.getMaxResource(Resource.Type.METAL);
        int maxCrystal = th.resources.getMaxResource(Resource.Type.CRYSTAL);

        // Pobierz obecne wartości surowców
        int wood = th.resources.GetResourceCount(Resource.Type.WOOD);
        int metal = th.resources.GetResourceCount(Resource.Type.METAL);
        int crystal = th.resources.GetResourceCount(Resource.Type.CRYSTAL);

        // Jedzenie
        int maxFood = th.resources.GetMaxFood();
        int food = maxFood - th.resources.GetFreeFood();

        // Update
        uiDisplayWood.UpdateUI(wood, maxWood);
        uiDisplayMetal.UpdateUI(metal, maxMetal);
        uiDisplayCrystal.UpdateUI(crystal, maxCrystal);
        uiDisplayFood.UpdateUI(food, maxFood);
    }
}
