using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    TransitionController _transitionController;

    private void Start()
    {
        _transitionController = GameObject.Find("TransitionController").GetComponent<TransitionController>();        
    }

    public void LoadMap()
    {
        StartCoroutine(LoadWithTransition());
    }

    IEnumerator LoadWithTransition()
    {
        _transitionController.EndTransition();
        yield return new WaitForSeconds(_transitionController.Delay);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
