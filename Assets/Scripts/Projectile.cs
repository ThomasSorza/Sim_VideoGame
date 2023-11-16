using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D projectileRb;
    public float speed;
    public float projectileLife;
    public float projectileCount;
     public PlayerMovement playerMovement;
    public bool facingRight;
    public int criticalDamage = 4;
    private bool isCritical = false; 

    // Start is called before the first frame update
    void Start()
    {
        speed = 14;
        projectileCount = projectileLife;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        facingRight = playerMovement.facingRight;
        if(!facingRight){
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }


    // Update is called once per frame
    void Update()
    {
     projectileCount -= Time.deltaTime;
     if(projectileCount<=0)   {
        Destroy(gameObject);
     }

    }

    private void FixedUpdate()
    {
        if(facingRight)
        {
             projectileRb.velocity = new Vector2(speed,projectileRb.velocity.y);
        }
        else
        {
           projectileRb.velocity = new Vector2(-speed,projectileRb.velocity.y);
        }
       
    }
    public void Initialize(bool isCritical)
    {
        this.isCritical = isCritical;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Enemy")
        {
            // Aplica el daño correspondiente (normal o crítico)
            if (isCritical)
            {
                 
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(criticalDamage);
            }
            else
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
