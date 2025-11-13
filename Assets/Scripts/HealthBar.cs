
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image healthBarImage;
    private float totalHealth = 0.0f;

    public CollisionDamageLerper[] limbArray;
    void Start()
    {
        //PopulateHealth();
    }

    public void DamageHealthBarValue(float damage_amount)
    {
        //print("Incoming damage is " + damage_amount);
        healthBarImage.fillAmount -= damage_amount / totalHealth;
        //print("Health bar fill amount is now " + healthBarImage.fillAmount);
    }

    public void HealHealthBar(float heal_amount)
    {
        float num_dead = 0.0f;
        foreach (CollisionDamageLerper i in limbArray)
        {
            if (i.isDead)
            {
                num_dead += 1.0f * i.maxHealthLevel;
            }
        }
        healthBarImage.fillAmount += heal_amount / (totalHealth - num_dead);
    }

    public void PopulateHealth()
    {
        foreach(CollisionDamageLerper i in limbArray)
        {
            totalHealth += i.maxHealthLevel;
        }
        //print("Total health is " + totalHealth);
        healthBarImage.fillAmount = 1.0f;
    }

}
