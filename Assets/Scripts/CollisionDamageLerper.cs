using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageLerper : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Renderer rend;
    
    

    public Material damagedMaterial;
    public Material fullHealthMaterial;
    public HealthBar healthBar;

    public float forceThreshold = 3.0f; //If the force of impact is higher than this, damage will be taken

    //Proto Health System, will be changed for per material and body part
    public float maxHealthLevel = 100.0f; //Most healthy at 0.0, might change logic to reverse if unintuitive
    private float currentHealthLevel = 0.0f;
    //public float damageFromImpact = 0.25f;
    
    void Start()
    {
        currentHealthLevel = maxHealthLevel;
        DamageMesh(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void DamageMesh(float damage_from_impact)
    {
        if (damage_from_impact > forceThreshold)

            currentHealthLevel -= damage_from_impact;
            float normalized_health = currentHealthLevel / maxHealthLevel;
            rend.material.Lerp(damagedMaterial, fullHealthMaterial, normalized_health);
            if(healthBar != null)
                healthBar.SetHealthBarValue(normalized_health);
    }
    
    
}
