using Assets.Scripts;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        inputActions.UI.Click.performed += OnleftClick;

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
            HandClaw.GetComponent<SpriteRenderer>().sprite = ClosedSprite;
    }
    public List<RaycastResult> GetRayResults()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = inputActions.UI.Point.ReadValue<Vector2>(),
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results;
    }
    public void OnleftClick(InputAction.CallbackContext ctx)
    {
        List<RaycastResult> results = GetRayResults();

        foreach (RaycastResult r in results)
        {
            if (r.gameObject.GetComponent<IClickable>() != null)
            {
                r.gameObject.GetComponent<IClickable>().OnLeftClick();
                Debug.Log(r.gameObject);
                break;
            }
            
        }

    }
}
