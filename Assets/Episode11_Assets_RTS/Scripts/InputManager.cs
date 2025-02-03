using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;

    [SerializeField]  private Vector3 lastPosition;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        /*
        Old Input System

        if (Input.GetMouseButtonDown(0))
             OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
             OnExit?.Invoke();
        */

        // New Input System

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClicked?.Invoke();
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
        { 
            OnExit?.Invoke(); 
        }
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();


    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
