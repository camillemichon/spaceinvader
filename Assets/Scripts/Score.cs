using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text highScore;
    public HighScore hs;
    private int score;

    private void Start()
    {
        highScore.text = hs.text;
    }

    private void OnEnable()
    {
        Enemy.OnEnemyTouch += UpdateScore;
    }

    private void OnDisable()
    {  
        Enemy.OnEnemyTouch -= UpdateScore;
    }

    private void UpdateScore(int point)
    {
        score += point;
        scoreText.text = "SCORE\n" + NumberZero() + score;
        SaveHighScore();
    }

    private string NumberZero()
    {
        int backup = score;
        string zeros = "0000";
        while (backup >= 1)
        {
            backup = backup / 10;
            zeros = zeros.Substring(1);
        }

        return zeros;
    }

    private void SaveHighScore()
    {
        if (score > hs.score)
        {
            hs.score = score;
            hs.text = "HIGH" + scoreText.text;
        }
    }
}
