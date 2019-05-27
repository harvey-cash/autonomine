using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deposit : MonoBehaviour, IGridPlaceable
{
    public int Mine() { return 1; }

    public void Start() {
        GameManager.manager.PlaceAtGridPos(this, 0, 0, out bool success);
        if (!success) {
            throw new Exception();
        }
    }

    // ~~~~~ IGridPlaceable ~~~~~~~ //
    private Vector2Int gridCoords;

    public Transform GetTransform() {
        return transform;
    }

    public Vector2Int CurrentCoords() {
        return gridCoords;
    }

    public void SetCoords(Vector2Int coords) {
        gridCoords = coords;
    }
}
