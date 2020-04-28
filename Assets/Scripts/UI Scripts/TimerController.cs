using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float currentTime = 0f;
    public float startingTime = 99f;

    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        timer.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
