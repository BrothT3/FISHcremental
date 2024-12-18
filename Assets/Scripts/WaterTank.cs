using Assets.Scripts;
using UnityEngine;

public class WaterTank : MonoBehaviour, IClickable
{
    public GameObject FoodPellet;
    public GameObject HookClaw;
    public void Awake()
    {

    }
    public void OnLeftClick()
    {
        Canvas c = FindAnyObjectByType<Canvas>();
        GameObject food = Instantiate(FoodPellet, transform.position, Quaternion.identity);

        food.transform.SetParent(c.transform, false);
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (c.GetComponent<RectTransform>(), RectTransformUtility.WorldToScreenPoint(Camera.main, HookClaw.transform.position),
        Camera.main, out canvasPosition);
        food.GetComponent<RectTransform>().anchoredPosition = canvasPosition;

        FishFood foodScript = food.GetComponent<FishFood>();
    }
}

