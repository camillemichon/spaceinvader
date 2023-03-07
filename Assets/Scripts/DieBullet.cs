using UnityEngine;

public class DieBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public float speed = 3;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
    }
    
    private void Fire()
    {
        myRigidbody2D.velocity = Vector2.down * speed;
    }
}
