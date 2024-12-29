using UnityEngine;

public class FeedButton : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        GameManager.Instance.IP.ClickType = CLICKTYPE.FEED;
    }
}
