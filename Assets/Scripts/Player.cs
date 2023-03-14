using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

  public AudioClip audioShoot;
  public AudioClip audioDie;

  private AudioSource audioSource;

  private Animator animator;
  
  private void Start()
  {
    hasLose = false;
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
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
      StartCoroutine(ShootAnimation());
      audioSource.clip = audioShoot;
      audioSource.Play();
      GameObject shot = Instantiate(bullet, shottingOffset.position, Quaternion.identity);
      Destroy(shot, 1.7f);

    }
  }

  private IEnumerator ShootAnimation()
  {
    animator.SetBool("Shoot", true);
    yield return new WaitForSeconds(0.3f);
    animator.SetBool("Shoot", false);
  }

  private IEnumerator DeathAnimation()
  {
    animator.SetBool("Death", true);
    yield return new WaitForSeconds(0.3f);
    animator.SetBool("Death", false);
  }
  
  private IEnumerator GameToCredits()
  {
    // wait for the player death animation to play
    yield return new WaitForSeconds(0.4f);

    SceneManager.LoadScene("Credits");
  }

  private void OnTriggerEnter2D(Collider2D pwew)
  {
    if (pwew.CompareTag("Enemy"))
    {          
       lives--;
       Debug.Log("Player lives : " + lives);
       
       audioSource.clip = audioDie;
       audioSource.Play();

       StartCoroutine(DeathAnimation());
       
       if (lives == 0)
       {
         StartCoroutine(GameToCredits());
         hasLose = true;
         OnPlayerLose?.Invoke();
       }
    }

    Destroy(pwew.gameObject);
  }
}
