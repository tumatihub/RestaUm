using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _spawnBulletPoint;
    [SerializeField] float _bulletSpeed;

    Animator _anim;
    [SerializeField] ParticleSystem _smokeRight;
    [SerializeField] ParticleSystem _smokeLeft;

    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
    }

    public override void Shoot()
    {
        print("Cannon Shooting!");
        var _bullet = Instantiate(_bulletPrefab, _spawnBulletPoint.transform.position, _spawnBulletPoint.transform.rotation);
        _bullet.GetComponent<Rigidbody>().velocity = _spawnBulletPoint.transform.forward * _bulletSpeed;
        _bullet.GetComponent<CannonBullet>().parentWeapon = this;
        _smokeLeft.Play();
        _smokeRight.Play();
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
