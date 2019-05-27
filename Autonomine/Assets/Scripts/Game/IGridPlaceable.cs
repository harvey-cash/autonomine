using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridPlaceable {

    Vector2Int CurrentCoords();
    void SetCoords(Vector2Int coords);

    Transform GetTransform();
}
