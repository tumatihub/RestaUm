using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject _winPanelPrefab;
    [SerializeField] GameObject _defeatPanelPrefab;

    GameObject _mainCanvas;

    PlayerController _player;

    IEnumerator _checkVictoryOrDefeatCoroutine;

    private void Start()
    {
        _weaponPanel = GameObject.Find("WeaponPanel").GetComponent<VerticalLayoutGroup>();
        _player = FindObjectOfType<PlayerController>();
        _mainCanvas = GameObject.Find("Canvas");
        GenerateWeaponsPanel();
        _checkVictoryOrDefeatCoroutine = CheckVictoryOrDefeat();
        StartCoroutine(_checkVictoryOrDefeatCoroutine);
    }

    IEnumerator CheckVictoryOrDefeat()
    {
        while (true)
        {
            if (!_player.isShooting && !IsThereWeaponActive())
            {
                if (FindObjectsOfType<Enemy>().Length == 0)
                {
                    Victory();
                }
                else
                {
                    if (!IsThereWeaponAvailable())
                    {
                        Defeat();
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private bool IsThereWeaponActive()
    {
        var _wpns = FindObjectsOfType<Weapon>();
        foreach(var _wpn in _wpns)
        {
            if (_wpn.IsActive)
                return true;
        }
        return false;
    }

    public void Defeat()
    {
        StopCoroutine(_checkVictoryOrDefeatCoroutine);
        Instantiate(_defeatPanelPrefab, _mainCanvas.transform);
    }

    private bool IsThereWeaponAvailable()
    {
        foreach(var _wpn in _listOfWeapons)
        {
            if (_wpn.numOfWeapons > 0)
                return true;
        }
        return false;
    }

    public void Victory()
    {
        StopCoroutine(_checkVictoryOrDefeatCoroutine);
        Instantiate(_winPanelPrefab, _mainCanvas.transform);
    }

    public void NextMap()
    {
        var _currentMapIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentMapIndex + 1);
    }

    public void RestartMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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

    public void ReturnWeaponByIndex(int index)
    {
        _listOfWeapons[index].numOfWeapons++;
        UpdateWeaponQty(index);
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
