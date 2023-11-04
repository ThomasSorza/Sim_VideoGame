using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{   
    private Rigidbody2D rb;
    public int maxHealth = 10;
    public int health;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            Hurt();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Hurt()
    {
        anim.SetTrigger("hurt");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(10); //traps do 10 damage means instant death
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("dead");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restarting Level");
    }

}
