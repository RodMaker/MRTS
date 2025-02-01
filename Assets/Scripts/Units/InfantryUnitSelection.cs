using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantryUnitSelection : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Camera mainCamera;

    public List<InfantryUnit> selectedUnits { get; } = new List<InfantryUnit>();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            foreach (InfantryUnit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }

            selectedUnits.Clear();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SelectionArea();
        }
    }

    private void SelectionArea()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if (!hit.collider.TryGetComponent<InfantryUnit>(out InfantryUnit unit)) { return; }

        //if (!unit.hasAuthority) { return; }

        selectedUnits.Add(unit);

        foreach (InfantryUnit selectedUnit in selectedUnits)
        {
            selectedUnit.Select();
        }
    }
}
