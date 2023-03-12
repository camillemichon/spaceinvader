using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditToMain : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GoBackToMain());
    }

    private IEnumerator GoBackToMain()
    {
        yield return new WaitForSeconds(5);

        Destroy(Music.music);
        SceneManager.LoadScene("MainScene");
    }
}
