using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject HandClaw;
    public InputSystem_Actions inputActions;
    public Sprite ClosedSprite;
    public Sprite OpenSprite;
    public float ClawTimer;
    public void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
    }
    public void Update()
    {
        ClawFollowing();
    }

    public void ClawRelease()
    {
        ClawTimer = 0.5f;
    }

    public void ClawFollowing()
    {
        Vector2 mPos = inputActions.UI.Point.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, Camera.main.nearClipPlane));
        if (HandClaw != null)
            HandClaw.transform.position = new Vector2(worldPos.x, HandClaw.transform.position.y);

        ClawRelease();

        if (ClawTimer > 0 && OpenSprite != null)
            HandClaw.GetComponent<SpriteRenderer>().sprite = OpenSprite;
        else if (ClosedSprite != null)
            HandClaw.GetComponent <SpriteRenderer>().sprite = ClosedSprite;
    }
}
