using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to use Text class

public class ItemCollector : MonoBehaviour
{
    private int bananas = 0;
    //private int bananasToWin = 5; //check if this is the correct number to pass the level

    [SerializeField] private Text scoreText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            bananas++;
            scoreText.text = "Score: " + bananas;
        }
    }
}
