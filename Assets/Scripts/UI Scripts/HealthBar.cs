using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    //gradient healthbar feature
    public Gradient gradient;
    //fill attribute
    public Image fill;

    //set max health parameter
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;


        //Green fill color health at max
        fill.color = gradient.Evaluate(1f);
    }

    //adjust health bar slider
    public void SetHealth(int health)
    {
        slider.value = health;

        //sets proper fill color for specific health value, normalized bet 0-1
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
