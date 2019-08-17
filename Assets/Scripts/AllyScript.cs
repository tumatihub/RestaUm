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
        if (other.CompareTag("Weapon")) { return; }

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

    public virtual void PushBack(Vector3 impactDir)
    {
        RaycastHit _hitInfo;
        if (Physics.Raycast(_tileFloor.transform.position, impactDir, out _hitInfo, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            var _hitFloor = _hitInfo.transform.gameObject.GetComponent<TileFloor>();
            if (_hitFloor.enemy == null)
            {
                _tileFloor.enemy = null;
                _tileFloor = _hitInfo.transform.gameObject.GetComponent<TileFloor>();
                transform.position = _tileFloor.transform.position;
            }
        }
    }
}
