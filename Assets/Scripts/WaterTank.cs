using Assets.Scripts;
using UnityEngine;

public class WaterTank : MonoBehaviour, IClickable
{
    public GameObject FoodPellet;
    public GameObject HookClaw;
    public int MaxFoodPellets = 1;
    public void Awake()
    {
        MaxFoodPellets = 1;
    }
    public void OnLeftClick()
    {
        if (GameManager.Instance.IP.ClickType != CLICKTYPE.FEED)
            return;

        if (GetFoodPellets() >= MaxFoodPellets)
            return;
        if (HookClaw.transform.position.x < -950)
            return;
        
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
    public int GetFoodPellets()
    {
        return GameManager.Instance.FoodContainer.transform.childCount;
    }
}

