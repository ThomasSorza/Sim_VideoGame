using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

        for (int h = 0; h < hearts.Length; h++)
        {
            if (h < health)
            {
                hearts[h].sprite = fullHeart;
            }
            else
            {
                hearts[h].sprite = emptyHeart;
            }
            if (h < maxHealth)
            {
                hearts[h].enabled = true;
            }
            else
            {
                hearts[h].enabled = false;
            }
            if (h < health)
            {
                hearts[h].sprite = fullHeart;
            }
            else
            {
                hearts[h].sprite = emptyHeart;
            }
        }
    }
}
