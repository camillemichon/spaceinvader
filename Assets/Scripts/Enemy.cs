using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public delegate void TouchEnemy(int point);
    public static event TouchEnemy OnEnemyTouch;


    public GameObject bulletPrefab;
    public int point;

    private void Update()
    {
        Random random = new Random();
        if (bulletPrefab != null && random.Next(100000) < 10)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D pwew)
    {
        if (pwew.CompareTag("Enemy")) return;
        
        OnEnemyTouch?.Invoke(point);
        Destroy(pwew.gameObject);
        Destroy(gameObject);
    }
}
