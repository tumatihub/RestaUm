using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    protected TileFloor _tileFloor;

    private void Start()
    {
        _tileFloor = GetTileFloor();
        _tileFloor.ally = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        var _levelController = FindObjectOfType<LevelController>();
        _levelController.Defeat();
        Die();
    }

    private TileFloor GetTileFloor()
    {
        RaycastHit _hitInfo;
        var orig = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
        if (Physics.Raycast(orig, Vector3.down, out _hitInfo, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            return _hitInfo.transform.gameObject.GetComponent<TileFloor>();
        }
        return null;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
