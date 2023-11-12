using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage =2;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.isKnockedFromRight = true;
            }
            if(collision.transform.position.x >= transform.position.x)
            {
                playerMovement.isKnockedFromRight = false;
            }
            anim.SetTrigger("Hit");
            playerHealth.TakeDamage(damage);
        }
    }
}
