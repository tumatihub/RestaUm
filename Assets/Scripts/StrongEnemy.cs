using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy
{
    [SerializeField] Material _lowHealthMaterial;
    MeshRenderer _mesh;

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
            _mesh.material = _lowHealthMaterial;
        }

        return _hitPointsRemaining;
    }
}
