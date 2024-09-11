using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentHealth, float maxHealth )
    {
        slider.gameObject.SetActive(true);
        if(currentHealth < 0 ) currentHealth = 0;
        slider.value = currentHealth / maxHealth; 
    }
}
