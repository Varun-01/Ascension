using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionBar : MonoBehaviour
{

    public Slider slider;

    //gradient tensionbar feature
    public Gradient gradient;
    //fill attribute
    public Image fill;

    //set max tension parameter
    public void SetMinTension(int tension)
    {
        slider.minValue = tension;
        slider.value = tension;


        //Cyan fill color tension at max
        fill.color = gradient.Evaluate(1f);
    }

    //adjust health bar slider
    public void SetTension(int tension)
    {
        slider.value = tension;

        //sets proper fill color for specific health value, normalized bet 0-1
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
