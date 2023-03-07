using UnityEngine;

public class Barricade : MonoBehaviour
{
    public Sprite secondPhase;
    private int lives = 2;

    private void OnTriggerEnter2D(Collider2D pwew)
    {
        lives--;
        
        Destroy(pwew.gameObject);
        
        if (lives == 0)
            Destroy(gameObject);
        else
            GetComponent<SpriteRenderer>().sprite = secondPhase;
    }
}
