using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction<Vector2> EdgeScroll;
    public static void OnEdgeScroll(Vector2 val) => EdgeScroll?.Invoke(val);

    // Tryb budowania
    public static UnityAction<GameObject> OnBuildingModeStarted;
    public static UnityAction OnBuildingModeEnded;

    // Budowanie budynków
    public static UnityAction<Structure> OnBuildingFinished;
}
