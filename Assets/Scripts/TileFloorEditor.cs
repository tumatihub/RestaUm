using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(TileFloor))]
public class TileFloorEditor : MonoBehaviour
{
    TileFloor tileFloor;

    private void Awake()
    {
        tileFloor = GetComponent<TileFloor>();
    }

    void Update()
    {
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        int gridSize = tileFloor.GetGridSize();
        transform.position = new Vector3(
            tileFloor.GetGridPos().x * gridSize,
            transform.position.y,
            tileFloor.GetGridPos().y * gridSize
        );
        gameObject.name = tileFloor.GetGridPos().x.ToString() + ", " + tileFloor.GetGridPos().y.ToString();
    }
}
