using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    Dictionary<Vector2Int, TileFloor> grid = new Dictionary<Vector2Int, TileFloor>();

    // Start is called before the first frame update
    void Start()
    {
        LoadTiles();
    }

    private void LoadTiles()
    {
        var tileFloors = FindObjectsOfType<TileFloor>();
        foreach (TileFloor tileFloor in tileFloors)
        {
            var gridPos = tileFloor.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + tileFloor);
            }
            else
            {
                grid.Add(gridPos, tileFloor);
            }
        }
    }
}
