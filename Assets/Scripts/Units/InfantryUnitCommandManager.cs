using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantryUnitCommandManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private InfantryUnitSelection unitSelection = null;

    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        MoveUnit(hit.point);
    }

    private void MoveUnit(Vector3 point)
    {
        foreach (InfantryUnit unit in unitSelection.selectedUnits)
        {
            unit.GetUnitMovement().CmdMove(point);
            unit.GetUnitMovement().agent.SetDestination(point);
        }
    }
}
