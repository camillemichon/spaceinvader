using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static GameObject music;
    void Start()
    {
        music = gameObject;
        DontDestroyOnLoad(this);
    }

}
