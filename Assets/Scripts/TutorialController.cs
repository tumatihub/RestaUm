using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;

    Animator _arrowAnim;

    PlayerController _playerController;

    bool _selectingWeapon = true;
    bool _placing = false;
    bool _shooting = false;

    [SerializeField] TileFloor _tileToPlace;
    [SerializeField] GameObject _selectorPrefab;

    GameObject _selector;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _arrowAnim = GameObject.Find("ui_arrow").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectingWeapon)
        {
            if (_playerController.WeaponToPlace != null)
            {
                _arrowAnim.SetBool("placing", true);
                _placing = true;
                _selectingWeapon = false;
            }
        }
        
        if (_placing)
        {
            if (_selector == null)
            {
                _selector = Instantiate(_selectorPrefab);
                _selector.transform.position = new Vector3(_tileToPlace.transform.position.x, _selector.transform.position.y, _tileToPlace.transform.position.z);
            }
            if (_playerController.WeaponToPlace == null)
            {
                _arrowAnim.SetBool("shooting", true);
                _placing = false;
                _shooting = true;
                Destroy(_selector.gameObject);
            }
        }

        if (_shooting)
        {
            if (!_playerController.SelectedWeapon.GetComponent<Weapon>().IsActive)
            {
                _arrowAnim.SetBool("shooting", false);
                _shooting = false;
            }
        }
    }
}
