using System;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 1;
    public GameObject gameOverPrefab;
    
    private int direction = 1;
    private float nbEnemies;
    private float acceleration;


    private void OnEnable()
    {
        Player.OnPlayerLose += OnLose;
        Enemy.OnEnemyTouch += IncreaseSpeed;
    }


    private void OnDisable()
    {
        Player.OnPlayerLose -= OnLose;
        Enemy.OnEnemyTouch -= IncreaseSpeed;
    }

    private void Start()
    {
        nbEnemies = transform.childCount;
        acceleration = 1 / nbEnemies;
    }

    void Update()
    {
        if (Player.hasLose) return;
        
        Vector3 position = transform.position;
        position.x += Vector2.right.x * direction * Time.deltaTime * speed;
        transform.position = position;

        foreach (Transform enemy in transform)
        {
            if (enemy.position.x is < -7 or > 7)
            {
                position.y += Vector2.down.y * 0.2f;
                transform.position = position;
                direction *= -1;
                return;
            }
        }
    }

    private void OnLose()
    {
        foreach (Transform enemy in transform)
        {
            Destroy(enemy.gameObject);
        }

        gameOverPrefab.SetActive(true);
    }

    private void IncreaseSpeed(int points)
    {
        speed += acceleration;
    }
}
