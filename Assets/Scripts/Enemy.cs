﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class Enemy : MonoBehaviour
{
    protected int _hitPoints;
    protected TileFloor _tileFloor;
    [SerializeField] protected int _maxHitPoints;

    protected AudioSource _audioSouce;
    [SerializeField] protected AudioClip _deathSFX;

    protected virtual void Start()
    {
        _hitPoints = _maxHitPoints;
        _tileFloor = GetTileFloor();
        _audioSouce = GetComponent<AudioSource>();
        if (_tileFloor == null)
            Debug.LogError("Can´t find the tileFloor of the enemy " + name);
        else
            _tileFloor.enemy = this;
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

    public virtual int TakeDamage(int amount, Vector3 impactDir)
    {
        _hitPoints -= amount;
        if (_hitPoints <= 0)
        {
            Die();
        }
        else
        {
            PushBack(impactDir);
        }
        return _hitPoints;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void PushBack(Vector3 impactDir)
    {
        RaycastHit _hitInfo;
        if (Physics.Raycast(_tileFloor.transform.position, impactDir, out _hitInfo, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            var _hitFloor = _hitInfo.transform.gameObject.GetComponent<TileFloor>();
            if (_hitFloor.enemy == null && _hitFloor.ally == null)
            {
                _tileFloor.enemy = null;
                _tileFloor = _hitInfo.transform.gameObject.GetComponent<TileFloor>();
                transform.position = _tileFloor.transform.position;
            }
        }
    }
}
