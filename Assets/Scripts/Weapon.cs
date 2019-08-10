using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon: MonoBehaviour
{
    protected bool _isActive = true;
    Button _shootButton;
    PlayerController _player;

    private void Start()
    {
        _shootButton = GameObject.Find("ShootButton").GetComponent<Button>(); // TODO: Remove string
        if (_shootButton == null)
            Debug.LogError("Need a ShootButton");

        _player = FindObjectOfType<PlayerController>();
        if (_player == null)
            Debug.LogError("Couldn´t find the playerController!");
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

    public void EndTurn()
    {
        _player.isShooting = false;
    }

    public abstract void Shoot();
}
