using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingCannon : Weapon
{

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _spawnBulletPoint;
    [SerializeField] float _bulletSpeed;
    
    public override void Shoot()
    {
        print("PiercingCannon Shooting!");
        var _bullet = Instantiate(_bulletPrefab, _spawnBulletPoint.transform.position, _spawnBulletPoint.transform.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = _spawnBulletPoint.transform.forward * _bulletSpeed;
        _bullet.GetComponent<PiercingCannonBullet>().parentWeapon = this;
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
