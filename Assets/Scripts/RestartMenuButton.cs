using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartMenuButton : MonoBehaviour
{
    LevelController _levelController;
    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
    }

    public void RestartMap()
    {
        _levelController.RestartMap();
    }
}
