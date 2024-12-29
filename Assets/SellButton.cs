using UnityEngine;

public class SellButton : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        GameManager.Instance.IP.ClickType = CLICKTYPE.SELL;
    }
}
