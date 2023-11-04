using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int bananas = 0;
    //private int bananasToWin = 5; //check if this is the correct number to pass the level


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Banano")
        {
            Destroy(collision.gameObject);
            bananas++;
            Debug.Log("Bananas: " + bananas);
        }
    }
}
