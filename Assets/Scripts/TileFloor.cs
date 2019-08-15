using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFloor : MonoBehaviour
{
    public bool isPlaceable = true;

    Vector2Int gridPos;

    const int gridSize = 10;

    GameObject _placedWeapon;
    bool _haveWeapon = false;

    public Enemy enemy;
    public AllyScript ally;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public bool PlaceWeapon(GameObject weaponToPlace)
    {
        if (!_haveWeapon)
        {
            _placedWeapon = weaponToPlace;
            _haveWeapon = true;
            return true;
        }
        return false;
    }
}
