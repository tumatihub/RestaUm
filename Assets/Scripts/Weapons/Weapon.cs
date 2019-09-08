using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    protected bool _isActive = true;
    public bool IsActive { get { return _isActive; } }
    public Sprite Icon;
    public string Description;
    public string Name;
    public int Index;

    Button _shootButton;
    PlayerController _player;
    TileFloor _floor;

    protected virtual void Start()
    {
        var _shootButtonObj = GameObject.Find("ShootButton");
        if (_shootButtonObj != null)
        {
            _shootButton = GameObject.Find("ShootButton").GetComponent<Button>(); // TODO: Remove string
        }
        if (_shootButton == null)
            Debug.LogWarning("Need a ShootButton");

        _player = FindObjectOfType<PlayerController>();
        if (_player == null)
            Debug.LogWarning("Couldn´t find the playerController!");
    }

    public void SetFloor(TileFloor floor)
    {
        _floor = floor;
    }

    public void RemoveFloor()
    {
        _floor.RemoveWeapon();
        _floor = null;
    }

    public void Execute()
    {
        if (_isActive)
        {
            Shoot();
            _isActive = false;
            _player.isShooting = true;
            DeactivateShootButton();
        }
    }

    private void DeactivateShootButton()
    {
        _shootButton.interactable = false;
    }

    public virtual void EndTurn()
    {
        _player.isShooting = false;
    }

    public abstract void Shoot();

    public abstract void Activate();

    public abstract void Deactivate();
}
