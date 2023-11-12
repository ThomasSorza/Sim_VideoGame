using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to use Text class

public class ItemCollector : MonoBehaviour
{
    private int bananas = 0;
    //private int bananasToWin = 5; //check if this is the correct number to pass the level

    [SerializeField] private Text bananosText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Banano")
        {
            Destroy(collision.gameObject);
            bananas++;
            bananosText.text = "Bananas: " + bananas;
        }
    }
}
