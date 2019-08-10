﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _spawnBulletPoint;
    [SerializeField] float _bulletSpeed;
    
    public override void Shoot()
    {
        print("Cannon Shooting!");
        var _bullet = Instantiate(_bulletPrefab, _spawnBulletPoint.transform.position, _spawnBulletPoint.transform.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = _spawnBulletPoint.transform.forward * _bulletSpeed;
        _bullet.GetComponent<CannonBullet>().parentWeapon = this;
    }
}
