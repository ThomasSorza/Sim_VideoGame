using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Fruit_Collected : MonoBehaviour
{
    public AudioSource clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;

            // Verifica que hay al menos dos hijos antes de intentar acceder al segundo hijo.
            if (transform.childCount >= 0)
            {
                transform.GetChild().gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No hay suficientes hijos en el objeto actual.");
            }

            Destroy(gameObject, 0.5f);
            clip.Play();
        }
    }
}
