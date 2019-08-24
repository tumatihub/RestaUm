using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : Enemy
{
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] Transform _deathSpawnPoint;

    protected override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        Instantiate(_deathParticles, _deathSpawnPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
