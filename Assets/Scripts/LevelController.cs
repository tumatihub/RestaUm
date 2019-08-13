using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [System.Serializable]
    struct WeaponAvailable
    {
        [SerializeField] public GameObject weaponPrefab;
        [SerializeField] public int numOfWeapons;
        [HideInInspector] public GameObject weaponUI;
    }

    [SerializeField] WeaponAvailable[] _listOfWeapons;
    [SerializeField] GameObject _weaponUIPrefab;
    LayoutGroup _weaponPanel;

    PlayerController _player;

    private void Start()
    {
        _weaponPanel = GameObject.Find("WeaponPanel").GetComponent<VerticalLayoutGroup>();
        _player = FindObjectOfType<PlayerController>();
        GenerateWeaponsPanel();    
    }

    public GameObject GetWeaponPrefab(int index)
    {
        if (index < _listOfWeapons.Length && _listOfWeapons[index].numOfWeapons > 0)
        {
            return _listOfWeapons[index].weaponPrefab;
        }
        return null;
    }

    public void RemoveWeaponFromListByIndex(int index)
    {
        if (index < _listOfWeapons.Length && _listOfWeapons[index].numOfWeapons > 0)
        {
            _listOfWeapons[index].numOfWeapons--;
            UpdateWeaponQty(index);
        }
    }

    private void UpdateWeaponQty(int index)
    {
        var _weaponUI = _listOfWeapons[index].weaponUI;
        _weaponUI.transform.Find("WeaponQty").GetComponent<Text>().text = _listOfWeapons[index].numOfWeapons.ToString();

    }

    private void GenerateWeaponsPanel()
    {
        for(var i = 0; i < _listOfWeapons.Length; i++)
        {
            int _index = i;
            var _weapon = _listOfWeapons[i];
            var _weaponUI = Instantiate(_weaponUIPrefab, _weaponPanel.transform);
            _weaponUI.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = _weapon.weaponPrefab.name;
            _weaponUI.transform.Find("WeaponQty").GetComponent<Text>().text = _weapon.numOfWeapons.ToString();
            _weaponUI.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => _player.GrabNewWeapon(_index));
            _listOfWeapons[i].weaponUI = _weaponUI;
        }
    }
}
