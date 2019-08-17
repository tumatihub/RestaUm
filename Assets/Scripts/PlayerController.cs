using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Button _shootButton;
    public bool isShooting = false;
    Button _removeButton;

    [SerializeField]
    GameObject _arrowPrefab;
    GameObject _arrow;

    // Start is called before the first frame update
    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
        if (_levelController == null)
            Debug.LogError("Need a LevelController object.");

        _shootButton = GameObject.Find("ShootButton").GetComponent<Button>(); // TODO: Remove string
        if (_shootButton == null)
            Debug.LogError("Need a shootButton.");

        _removeButton = GameObject.Find("RemoveButton").GetComponent<Button>(); // TODO: Remove string
        if (_removeButton == null)
            Debug.LogError("Need a removeButton.");
        _removeButton.onClick.AddListener(() => RemoveSelectedWeapon());
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

        CheckShortcutToNextLevel();
    }

    private void CheckShortcutToNextLevel()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
        {
            _levelController.NextMap();
        }
    }

    public void GrabNewWeapon(int index)
    {
        print("GrabWeapon: " + index);
        GameObject _prefab = _levelController.GetWeaponPrefab(index);
        _indexWeapon = index;

        if (_prefab != null)
        {
            if (_weaponToPlace != null)
            {
                Destroy(_weaponToPlace.gameObject);
            }
            _weaponToPlace = Instantiate(_prefab);
        }
    }

    private void RemoveSelectedWeapon()
    {
        if (_selectedWeapon != null && _selectedWeapon.GetComponent<Weapon>().IsActive)
        {
            _levelController.ReturnWeaponByIndex(_indexWeapon);
            Destroy(_selectedWeapon.gameObject);
            _selectedWeapon.GetComponent<Weapon>().RemoveFloor();
            _selectedWeapon = null;
            Destroy(_arrow.gameObject);
        }
    }

    private void LinkShootButtonToSelectedWeapon()
    {
        _shootButton.onClick.AddListener(() => _selectedWeapon.GetComponent<Weapon>().Execute());
    }

    private void SelectWeapon()
    {
        if (isShooting || _weaponToPlace != null) return;

        if (Input.GetMouseButtonDown(0))
        {
            print("Try to select");
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;

            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, _weaponLayerMask))
            {
                if (_hitInfo.transform.GetComponent<Weapon>().IsActive)
                {
                    _selectedWeapon = _hitInfo.transform.gameObject;
                    print("Selected: " + _selectedWeapon.name);
                    LinkShootButtonToSelectedWeapon();
                    _shootButton.interactable = true;
                    MoveArrowToSelectedWeapon();
                }
            }
        }
    }

    private void MoveArrowToSelectedWeapon()
    {
        if (_arrow == null)
        {
            _arrow = Instantiate(_arrowPrefab, _selectedWeapon.transform.position, Quaternion.identity);
        }

        _arrow.transform.position = _selectedWeapon.transform.position;
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
            _weaponToPlace.GetComponent<Weapon>().SetFloor(_floor);
            _weaponToPlace = null;
        }
    }
}
