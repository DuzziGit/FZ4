using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxHealth (int playerHealth){
        slider.maxValue = playerHealth;
        slider.value = playerHealth;
    }

    public void SetHealth(int playerHealth){
        slider.value = playerHealth;
    }

}
