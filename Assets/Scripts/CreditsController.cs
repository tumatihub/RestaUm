using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{

    [SerializeField] Weapon _wpn;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        _wpn.Activate();
        yield return new WaitForSeconds(1f);
        _wpn.Shoot();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }
}
