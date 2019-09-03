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

    AudioSource _audioSource;
    [SerializeField] AudioClip _shootSFX;
    [SerializeField] AudioClip _batteryChargingSFX;

    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        _audioSource.PlayOneShot(_shootSFX);
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
        _audioSource.PlayOneShot(_batteryChargingSFX);
    }

    public override void Deactivate()
    {
        _anim.SetTrigger("Deactivate");
    }
}
