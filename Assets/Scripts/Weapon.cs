using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    protected bool _isActive = true;

    public void Execute()
    {
        if (_isActive)
        {
            Shoot();
            _isActive = false;
        }
    }

    public abstract void Shoot();
}
