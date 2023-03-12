using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTransit : MonoBehaviour
{
    public void Transit()
    {
        SceneManager.LoadScene("MainGame");

    }
}
