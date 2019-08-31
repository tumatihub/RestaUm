using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    Animator _anim;
    Image _transitionImage;
    public float Delay = 1f;

    private void Start()
    {
        var _imageObj = GameObject.Find("TransitionImage");
        _transitionImage = _imageObj.GetComponent<Image>();
        _anim = _imageObj.GetComponent<Animator>();
    }

    public void EndTransition()
    {
        _anim.SetTrigger("EndTransition");
    }
}
