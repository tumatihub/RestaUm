﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy
{
    [SerializeField] Material _lowHealthMaterial;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] ParticleSystem _helmetParticles;
    [SerializeField] Transform _deathSpawnPoint;
    [SerializeField] AudioClip _armorHitSFX;

    protected override void Start()
    {
        base.Start();
    }

    public override int TakeDamage(int amount, Vector3 impactDir)
    {
        var _hitPointsRemaining = base.TakeDamage(amount, impactDir);

        if (_hitPointsRemaining == 1)
        {
            DropHelmet();
        }

        return _hitPointsRemaining;
    }

    private void DropHelmet()
    {
        transform.Find("EnemyHelmet").gameObject.SetActive(false);
        _audioSouce.PlayOneShot(_armorHitSFX);
        Instantiate(_helmetParticles, _deathSpawnPoint.transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        var obj = Instantiate(_deathParticles, _deathSpawnPoint.transform.position, Quaternion.identity);
        obj.GetComponent<AudioSource>().PlayOneShot(_deathSFX);
        Destroy(gameObject);
    }
}
