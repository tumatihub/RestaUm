using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [System.Serializable]
    struct WeaponAvailable
    {
        [SerializeField] public GameObject _weaponPrefab;
        [SerializeField] public int _numOfWeapons;
    }

    [SerializeField] WeaponAvailable[] _listOfWeapons;

    public GameObject GetWeaponPrefab(int index)
    {
        if (index < _listOfWeapons.Length && _listOfWeapons[index]._numOfWeapons > 0)
        {
            _listOfWeapons[index]._numOfWeapons--;
            return _listOfWeapons[index]._weaponPrefab;
        }
        return null;
    }
}
