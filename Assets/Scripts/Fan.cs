using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Weapon
{
    [SerializeField] Transform _windOrig;

    public override void Shoot()
    {
        print("Shooting Fan");
        RaycastHit _hitInfo;
        if (Physics.Raycast(_windOrig.transform.position, transform.forward, out _hitInfo, Mathf.Infinity))
        {
            if (_hitInfo.transform.CompareTag("Enemy"))
            {
                _hitInfo.transform.GetComponent<Enemy>().PushBack(transform.forward);
            }
            if (_hitInfo.transform.CompareTag("Ally"))
            {
                _hitInfo.transform.GetComponent<AllyScript>().PushBack(transform.forward);
            }
        }

        Invoke("EndTurn", 1f);
    }
    
}
