using System;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public float speed = 1;
    public GameObject gameOverPrefab;
    
    private int direction = 1;
    public static float nbEnemies;
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
        
        transform.position += Vector3.right * direction * Time.deltaTime * speed;

        foreach (Transform enemy in transform)
        {
            if (enemy.GetComponent<SpriteRenderer>().sprite == null) continue;
            
            if (enemy.position.x < -7 || enemy.position.x > 7)
            {
                direction *= -1;
                transform.position += Vector3.right * direction * Time.deltaTime * speed + Vector3.down * 0.3f;
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
