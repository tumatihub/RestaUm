using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;

    GameObject weaponToPlace;
    [SerializeField] LayerMask placebleLayerMask;
    [SerializeField] LayerMask FloorLayerMask;

    Vector3Int[] directions = { new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 1), new Vector3Int(0, 0, -1) };

    // Start is called before the first frame update
    void Start()
    {
    }

    private void GrabNewWeapon()
    {
        weaponToPlace = Instantiate(weaponPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponToPlace != null)
        {
            MoveWeaponToMouse();
        }

        if (Input.GetMouseButtonDown(1))
        {
            GrabNewWeapon();
        }
    }

    private void MoveWeaponToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, placebleLayerMask))
        {
            weaponToPlace.transform.position = hitInfo.transform.position;
            foreach(Vector3Int dir in directions)
            {
                if (Physics.Raycast(hitInfo.transform.position, dir, Mathf.Infinity, FloorLayerMask)){
                    weaponToPlace.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                PlaceWeapon(hitInfo);
            }
        }
    }

    private void PlaceWeapon(RaycastHit hitInfo)
    {
        TileFloor _floor = hitInfo.transform.gameObject.GetComponent<TileFloor>();
        if (_floor.PlaceWeapon(weaponToPlace))
        {
            weaponToPlace = null;
        }
    }
}
