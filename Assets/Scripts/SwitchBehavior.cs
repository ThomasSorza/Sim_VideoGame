using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : MonoBehaviour
{

    public GameObject waterObject;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float gravity = 3f;
            float w = 1.599635f * gravity;
            
            // Replace the placeholder with the actual size of the collider's x-axis
            float colliderSizeX = other.collider.bounds.size.x;

            // Assuming 'colliderSizeX' represents the width of the collider
            
            float pression = w / colliderSizeX;

            Debug.Log("Switch pressed with: " + pression + " N/m^2");
            if (waterObject != null)
            {
                WaterBehavior waterBehavior = waterObject.GetComponent<WaterBehavior>();
                if (waterBehavior != null)
                {
                    waterBehavior.RaiseWater();
                }
                else
                {
                    Debug.LogError("WaterBehavior component not found on the waterObject.");
                }
            }
            else
            {
                Debug.LogError("Water object not assigned to the 'waterObject' field.");
            }
        }
        else
        {
            Debug.Log("No pressed");
        }
}

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("No pressed");
        }
        else
        {
            Debug.Log("No pressed");
        }
    }
}
