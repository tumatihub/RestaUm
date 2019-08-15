using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    LevelController _levelController;
    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
    }

    public void LoadMainMenu()
    {
        _levelController.LoadMainMenu();
    }
}
