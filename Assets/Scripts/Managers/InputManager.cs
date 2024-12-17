using Assets.Scripts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputSystem_Actions inputActions;
    public bool LeftMouseClicked = false;
    public Vector2 mPos { get; private set; }
    public void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += OnleftClick;
        inputActions.UI.Click.canceled += OnLeftClickCancel;
        inputActions.UI.Point.performed += OnMouseMovement;
    }

    private void OnMouseMovement(InputAction.CallbackContext context)
    {
        mPos = context.ReadValue<Vector2>();
    }

    public void Update()
    {

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

    public void OnLeftClickCancel(InputAction.CallbackContext ctx)
    {
        LeftMouseClicked = false;
    }
    public void OnleftClick(InputAction.CallbackContext ctx)
    {
        if (LeftMouseClicked)
            return;
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
        LeftMouseClicked = true;
    }
}
