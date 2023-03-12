using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public delegate void TouchEnemy(int point);
    public static event TouchEnemy OnEnemyTouch;

    public AudioClip audioshoot;
    public AudioClip audiodie;

    private Animator animator;
    private ParticleSystem particle;
    
    public GameObject bulletPrefab;
    public int point;

    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        Random random = new Random();
        if (bulletPrefab != null && random.Next(100000) < 50)
        {
            audioSource.clip = audioshoot;
            audioSource.Play();
            
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D pwew)
    {
        if (pwew.CompareTag("Enemy") || pwew.CompareTag("Player")) return;

        MoveEnemy.nbEnemies--;
        if (MoveEnemy.nbEnemies == 0)
        {
            StartCoroutine(GotoCredits());
        }
        particle.Play();
        audioSource.clip = audiodie; 
        audioSource.Play();
        OnEnemyTouch?.Invoke(point);
        Destroy(pwew.gameObject);
        
        animator.SetTrigger("Death");
    }

    private IEnumerator GotoCredits()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Credits");
    }
}
