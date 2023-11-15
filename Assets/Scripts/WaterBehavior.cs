using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public float waterSpeed = 10f;
    public float maxHeight = 9.7f;
    public float minHeight = 3.7f;

    private bool isWaterUp = false;
    // Escala inicial del objeto
    public Vector3 escalaInicial = new Vector3(1f, 1f, 1f);

    // Escala final del objeto
    public Vector3 escalaFinal = new Vector3(2f, 2f, 2f);

    void Start()
    {
       // this.transform.localScale = escalaInicial;
        //RaiseWater();
    }
    void Update()
    {
        
    }

    // Método para subir el agua
    public void RaiseWater()
    {
        if (!isWaterUp && transform.localScale.y < maxHeight)
        {
            float newHeight = Mathf.Min(maxHeight, transform.localScale.y + waterSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
        }
    }

    // Método para bajar el agua
    public void LowerWater()
    {
        if (isWaterUp && transform.localScale.y > minHeight)
        {
            float newHeight = Mathf.Max(minHeight, transform.localScale.y - waterSpeed * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
        }
    }
}
