using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelIndicator : MonoBehaviour
{
    private float currentFuel;
    public Slider slider;
    public Image sliderFill;

    void Start()
    {

    }

    void Update()
    {
        currentFuel = GetComponent<Controller>().getFuel();
        slider.value = currentFuel;
        
        sliderFill.color = Color.Lerp(Color.red, Color.green, slider.value / 100);
    }
}
