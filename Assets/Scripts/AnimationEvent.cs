using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{

    public GameObject canvas;
    public void OnDeathAnimFinish()
    {
        canvas.SetActive(true);
    }
}
