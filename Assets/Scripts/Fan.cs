using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Weapon
{
    [SerializeField] Transform _windOrig;
    Animator _anim;

    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
    }

    public override void Shoot()
    {
        print("Shooting Fan");
        _anim.SetBool("Rotating", true);
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

    public override void EndTurn()
    {
        base.EndTurn();
        _anim.SetBool("Rotating", false);
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

}
