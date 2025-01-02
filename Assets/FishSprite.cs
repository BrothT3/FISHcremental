using Assets.Scripts.FSM;
using UnityEngine;

public class FishSprite : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        if (!GetComponentInParent<FISH>().notDying)
            return;
        if (GameManager.Instance.IP.ClickType != CLICKTYPE.SELL)
            return;

        Destroy(gameObject.transform.parent.gameObject);
    }
    public void SelfDestruct()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
