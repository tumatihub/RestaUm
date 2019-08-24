using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy
{
    [SerializeField] Material _lowHealthMaterial;
    MeshRenderer _mesh;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] ParticleSystem _helmetParticles;
    [SerializeField] Transform _deathSpawnPoint;

    protected override void Start()
    {
        base.Start();
        _mesh = transform.Find("EnemyBody").GetComponent<MeshRenderer>();
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
        Instantiate(_helmetParticles, _deathSpawnPoint.transform.position, Quaternion.identity);
    }

    public override void Die()
    {
        Instantiate(_deathParticles, _deathSpawnPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
