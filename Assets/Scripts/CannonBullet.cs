using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    private int _bulletDamage = 1;
    public Cannon parentWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(_bulletDamage, gameObject.transform.forward);
        }
        var _trail = transform.Find("Trail");
        _trail.parent = null;
        _trail.GetComponent<ParticleSystem>().Stop();
        Destroy(_trail.gameObject, 5f);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        parentWeapon.EndTurn();
    }
}
