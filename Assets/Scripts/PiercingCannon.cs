using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingCannon : Weapon
{

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _spawnBulletPoint;
    [SerializeField] float _bulletSpeed;

    Animator _anim;
    [SerializeField] ParticleSystem _smoke;

    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
    }

    public override void Shoot()
    {
        print("PiercingCannon Shooting!");
        var _bullet = Instantiate(_bulletPrefab, _spawnBulletPoint.transform.position, _spawnBulletPoint.transform.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = _spawnBulletPoint.transform.forward * _bulletSpeed;
        _bullet.GetComponent<PiercingCannonBullet>().parentWeapon = this;
        _smoke.Play();
        Deactivate();
    }

    public override void Activate()
    {
        _anim.SetTrigger("Activate");
    }

    public override void Deactivate()
    {
        _anim.SetTrigger("Deactivate");
    }
}
