using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _cannonPrefab;
    [SerializeField] GameObject _piercingCannonPrefab;

    GameObject _weaponToPlace;
    int _indexWeapon;
    GameObject _selectedWeapon;

    [SerializeField] LayerMask _placebleLayerMask;
    [SerializeField] LayerMask _floorLayerMask;
    [SerializeField] LayerMask _weaponLayerMask;


    Vector3Int[] _directions = { new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1) };

    LevelController _levelController;

    // Start is called before the first frame update
    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
        if (_levelController == null)
            Debug.LogError("Need a LevelController object.");
    }


    // Update is called once per frame
    void Update()
    {
        SelectWeapon();

        if (_selectedWeapon != null && Input.GetKeyDown(KeyCode.Space))
        {
            _selectedWeapon.GetComponent<Weapon>().Execute();
        }

        if (_weaponToPlace != null)
        {
            MoveWeaponToMouse();
        }
        
        GrabNewWeapon();
    }

    private void GrabNewWeapon()
    {
        GameObject _prefab = null;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _prefab = _levelController.GetWeaponPrefab(0);
            _indexWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _prefab = _levelController.GetWeaponPrefab(1);
            _indexWeapon = 1;
        }

        if (_prefab != null)
        {
            if (_weaponToPlace != null)
            {
                Destroy(_weaponToPlace.gameObject);
            }
            _weaponToPlace = Instantiate(_prefab);
        }
    }

    private void SelectWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Try to select");
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _weaponLayerMask))
            {
                _selectedWeapon = _hitInfo.transform.gameObject;
                print("Selected: " + _selectedWeapon.name);
            }
        }
    }

    private void MoveWeaponToMouse()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hitInfo;

        if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _placebleLayerMask))
        {
            _weaponToPlace.transform.position = _hitInfo.transform.position;
            foreach(Vector3Int dir in _directions)
            {
                if (Physics.Raycast(_hitInfo.transform.position, dir, Mathf.Infinity, _floorLayerMask)){
                    _weaponToPlace.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlaceWeapon(_hitInfo);
            }
        }
    }

    private void PlaceWeapon(RaycastHit hitInfo)
    {
        TileFloor _floor = hitInfo.transform.gameObject.GetComponent<TileFloor>();
        if (_floor.PlaceWeapon(_weaponToPlace))
        {
            _levelController.RemoveWeaponFromListByIndex(_indexWeapon);
            _weaponToPlace = null;
        }
    }
}
