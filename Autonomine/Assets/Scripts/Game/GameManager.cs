using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private void Awake() {
        manager = this;
        InitialiseTick();
    }

    private void Start() {
        InitialisePlayer();
    }

    /* ~~~~~~ PLAYER ~~~~~~ */

    public PlayerMachine player;

    private void InitialisePlayer() {
        UIManager.ui.Focus(player);
    }

    /* ~~~~~~ GAME TICK ~~~~~~ */

    public UnityEvent onTick;

    private void InitialiseTick() {
        if (onTick == null) { onTick = new UnityEvent(); }
        InvokeRepeating("DoTick", Config.TICK_TIME, Config.TICK_TIME);
    }

    private void DoTick() {
        onTick.Invoke();
    }

    /* ~~~~~~ GRID ~~~~~~ */

    private int sizeX = 100, sizeZ = 100;
    public readonly IGridPlaceable[,] grid = new IGridPlaceable[100,100];

    private Vector2Int Coords(IGridPlaceable gridObject) {
        int x = Mathf.FloorToInt(gridObject.GetTransform().position.x);
        int z = Mathf.FloorToInt(gridObject.GetTransform().position.z);

        int xC = (sizeX / 2) + x;
        int xZ = (sizeZ / 2) + z;

        return new Vector2Int(xC, xZ);
    }

    public void PlaceAtGridPos(IGridPlaceable gridObject, int dx, int dz, out bool success) {
        Vector2Int currentCoords = Coords(gridObject);

        Vector2Int newCoords = currentCoords + new Vector2Int(dx, dz);

        if (grid[newCoords.x, newCoords.y] == null) {
            success = true;
            grid[currentCoords.x, currentCoords.y] = null;
            grid[newCoords.x, newCoords.y] = gridObject;
            gridObject.SetCoords(newCoords);

            // Set
            gridObject.GetTransform().position = new Vector3(
                newCoords.x - (sizeX / 2),
                gridObject.GetTransform().position.y,
                newCoords.y - (sizeZ / 2)
            );
        }
        else {
            success = false;            
        }
    }

    public IGridPlaceable[] CheckAdjacent(IGridPlaceable gridObject) {
        Vector2Int coords = Coords(gridObject);
        Vector2Int[] adjacentCoords = new Vector2Int[] {
            coords + Vector2Int.up,
            coords + Vector2Int.right,
            coords + Vector2Int.down,
            coords + Vector2Int.left
        };
        IGridPlaceable[] adjacentObjects = new IGridPlaceable[adjacentCoords.Length];
        for (int i = 0; i < adjacentCoords.Length; i++) {
            adjacentObjects[i] = grid[adjacentCoords[i].x, adjacentCoords[i].y];
        }

        return adjacentObjects;
    }
}
