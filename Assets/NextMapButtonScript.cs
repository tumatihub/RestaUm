using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMapButtonScript : MonoBehaviour
{
    LevelController _levelController;
    void Start()
    {
        _levelController = FindObjectOfType<LevelController>();
    }

    public void NextMap()
    {
        _levelController.NextMap();
    }
}
