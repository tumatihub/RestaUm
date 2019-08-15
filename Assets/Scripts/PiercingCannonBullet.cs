using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingCannonBullet : MonoBehaviour
{
    private int _bulletDamage = 1;
    private int _piercedEnemies;
    private int _maxPiercedEnemies = 2;
    public PiercingCannon parentWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Edge") || other.gameObject.CompareTag("Ally"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            int _enemyHitPoints;
            print("Colliding with: " + other.gameObject.name);
            _enemyHitPoints = other.gameObject.GetComponentInParent<Enemy>().TakeDamage(_bulletDamage, gameObject.transform.forward);
            if (_enemyHitPoints > 0)
            {
                Destroy(gameObject);
            }
            else
            {
                if (_piercedEnemies >= _maxPiercedEnemies)
                {
                    Destroy(gameObject);
                }
                _piercedEnemies++;
            }
        }
    }

    private void OnDisable()
    {
        parentWeapon.EndTurn();
    }
}
