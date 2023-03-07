using UnityEngine;

public class Player : MonoBehaviour
{
  public delegate void PlayerLose();
  public static event PlayerLose OnPlayerLose;
  
  public GameObject bullet;
  public int speed = 2;
  public Transform shottingOffset;
  
  private Rigidbody2D rb;
  private int lives = 3;

  public static bool hasLose = false;
  
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    float mov = Input.GetAxis("Horizontal");
    rb.velocity = Vector2.right * mov * speed;
    if (transform.position.x > 7|| transform.position.x < -7)
    {
      transform.position = new Vector2(transform.position.x < 0 ? -7 : 7, transform.position.y);
      rb.velocity = Vector2.zero;
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      Destroy(shot, 1.7f);

    }
  }

  private void OnTriggerEnter2D(Collider2D pwew)
  {
    if (pwew.CompareTag("Enemy"))
    {          
       lives--;
       Debug.Log("Player lives : " + lives);
       
       if (lives == 0)
       {
         Destroy(gameObject);
         hasLose = true;
         OnPlayerLose?.Invoke();
       }
    }

    Destroy(pwew.gameObject);
  }
}
