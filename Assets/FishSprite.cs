using UnityEngine;

public class FishSprite : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        if (GameManager.Instance.IP.ClickType != CLICKTYPE.SELL)
            return;

        Destroy(gameObject.transform.parent.gameObject);
    }
}
