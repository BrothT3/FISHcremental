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

        food.transform.SetParent(GameManager.Instance.FoodContainer.transform, false);
        Vector2 newpos = new Vector2(HookClaw.transform.position.x, HookClaw.transform.position.y-30);
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (c.GetComponent<RectTransform>(), RectTransformUtility.WorldToScreenPoint(Camera.main, HookClaw.transform.position),
        Camera.main, out canvasPosition);
        food.GetComponent<RectTransform>().anchoredPosition = canvasPosition;

        FishFood foodScript = food.GetComponent<FishFood>();
    }
}

