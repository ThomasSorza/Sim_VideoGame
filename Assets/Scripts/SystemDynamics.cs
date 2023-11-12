using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemDynamics : MonoBehaviour
{
    public float timer = 0;
    private bool isButtonPressed = false;
    private List<int> buttonPressedValuesTime = new List<int>();
    private List<float> buttonStates = new List<float>();
    public Text timerText;
    public CanvasGroup timerCanvasGroup;
    private bool timerIsActive = false;
    private float buttonPressTimer = 0;
    public GameObject activationZone;
    public GameObject button;

    void Start()
    {
        timerCanvasGroup.alpha = 0;
    }

    void Update()
    {
        if (timerIsActive)
        {
            timer += Time.deltaTime;
            timerText.text = "" + timer.ToString("f0");

            buttonPressTimer += Time.deltaTime;
            if (buttonPressTimer >= 1)
            {
                buttonPressedValuesTime.Add(isButtonPressed ? 1 : 0);
                buttonStates.Add(CalculateButtonState());
                buttonPressTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered: " + other.gameObject.name);
            StartTimer(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited: " + other.gameObject.name);
            StopTimer(other.gameObject);
        }
    }

    void StartTimer(GameObject other)
    {
        if (other == activationZone)
        {
            Debug.Log("Player entered activation zone");
            timerCanvasGroup.alpha = 1;
            timerIsActive = true;
        }
        else if (other == button)
        {
            Debug.Log("Player pressed button");
            isButtonPressed = true;
        }
    }

    void StopTimer(GameObject other)
    {
        if (other == activationZone)
        {
            Debug.Log("Player exited activation zone");
            timerIsActive = false;
            timerCanvasGroup.alpha = 0;
        }
        else if (other == button)
        {
            Debug.Log("Player released button");
            isButtonPressed = false;
        }

        // Imprime el arreglo de valores
        Debug.Log("Button pressed values:");
        for (int i = 0; i < buttonPressedValuesTime.Count; i++)
        {
            Debug.Log("Index: " + i + ", Value: " + buttonPressedValuesTime[i]);
        }

        Debug.Log("Button states over the time values:");
        for (int i = 0; i < buttonStates.Count; i++)
        {
            Debug.Log("Index: " + i + ", Value: " + buttonStates[i]);
        }
    }

    float CalculateButtonState()
    {
        if (!isButtonPressed)
        {
            return 0;
        }

        int state = (int)(timer / 2) + 1;

        if (state > 4)
        {
            state = 4;
        }

        return state * 0.25f;
    }
}
