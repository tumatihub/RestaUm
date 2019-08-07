using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    private int _bulletDamage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            print("Colliding with: " + other.gameObject.name);
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(_bulletDamage, gameObject.transform.forward);
            Destroy(gameObject);
        }
    }
}
