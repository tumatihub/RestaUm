using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Weapon
{
    [SerializeField] Transform _windOrig;
    Animator _anim;
    [SerializeField] ParticleSystem _windParticles;

    [SerializeField] AudioClip _fanPropSFX;
    [SerializeField] AudioClip _batteryChargingSFX;
    AudioSource _audioSource;


    protected override void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        _anim.SetBool("Rotating", true);
        _windParticles.Play();
        _audioSource.PlayOneShot(_fanPropSFX);
        StartCoroutine(DelayedPush());
    }

    IEnumerator DelayedPush()
    {
        yield return new WaitForSeconds(1f);
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
        EndTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
        _anim.SetBool("Rotating", false);
        _audioSource.Stop();
    }

    public override void Activate()
    {
        _anim.SetTrigger("Activate");
        _audioSource.PlayOneShot(_batteryChargingSFX);
    }

    public override void Deactivate()
    {
    }

}
