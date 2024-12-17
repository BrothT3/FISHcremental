using UnityEngine;
using UnityEngine.InputSystem;

public class HookClaw : MonoBehaviour
{
    public Sprite ClosedSprite;
    public Sprite OpenSprite;
    public float ClawTimer;
    public void Update()
    {
        ClawFollowing();
    }
    public void ClawFollowing()
    {
        Vector2 mPos = GameManager.Instance.IP.mPos;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, Camera.main.nearClipPlane));
            transform.position = new Vector2(worldPos.x, transform.position.y);

        ClawRelease();

        if (ClawTimer > 0 && OpenSprite != null)
            GetComponent<SpriteRenderer>().sprite = OpenSprite;
        else if (ClosedSprite != null)
            GetComponent<SpriteRenderer>().sprite = ClosedSprite;
    }
    public void ClawRelease()
    {
        ClawTimer = 0.5f;
    }
}
