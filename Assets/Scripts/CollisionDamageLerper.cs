using System;
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

    public float maxHealthLevel = 100.0f;
    public bool canBeSevered = false;

    public GameObject severedLimbPrefab;
    [HideInInspector]
    public float currentHealthLevel = 0.0f;
    //private CharacterJoint connectedJoint;
    private GameObject connectedObject;

    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public GameObject fellaObject;
    [HideInInspector]
    public BodyPartManager bodyPartManager;
    //public float damageFromImpact = 0.25f;
    //public event EventHandler severed_limb;
    void Start()
    {
        currentHealthLevel = maxHealthLevel;
        //connectedJoint = gameObject.GetComponent<CharacterJoint>();
        connectedObject = rend.gameObject;
        DamageMesh(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void DamageMesh(float damage_from_impact)
    {
        if (damage_from_impact > forceThreshold && rend != null)
        {
            currentHealthLevel -= damage_from_impact;
            currentHealthLevel = Math.Clamp(currentHealthLevel, 0.0f, maxHealthLevel);

            float normalized_health = currentHealthLevel / maxHealthLevel;
            rend.material.Lerp(damagedMaterial, fullHealthMaterial, normalized_health);
            if (healthBar != null && currentHealthLevel >= 0.0f && !isDead)
            {
                healthBar.DamageHealthBarValue(Mathf.Clamp(damage_from_impact, 0.0f, maxHealthLevel));
            }
            if (currentHealthLevel <= 0.01f)
            {
                if (!isDead)
                {
                    if (canBeSevered) // Below is limb severing logic and checks
                    {
                        List<GameObject> connected_object_children = Tools.GetChildrenOfObject(connectedObject);
                        Mesh connected_shared_mesh = connectedObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                        InstanceSeveredLimb(connected_shared_mesh);

                        bodyPartManager.SetLimbDestroyedProperties(connected_shared_mesh);
                        
                        foreach (GameObject i in connected_object_children)
                        {
                            Mesh this_mesh = i.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                            CollisionDamageLerper potential_lerper_object = bodyPartManager.GetLerperFromMesh(this_mesh);

                            if (potential_lerper_object != null)
                            {

                                if (!potential_lerper_object.isDead)
                                {
                                    Mesh this_shared_mesh = i.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                                    InstanceSeveredLimb(this_shared_mesh);
                                    bodyPartManager.SetLimbDestroyedProperties(this_shared_mesh);
                                    print(i.gameObject + " Severing child limb");
                                }
                            }
                               
                        }

                        connectedObject.SetActive(false);

                    }
                }
                isDead = true;
                
            }
            
        }
    }
    public void HealMesh(float heal_amount)
    {
        currentHealthLevel += heal_amount;
        currentHealthLevel = Math.Clamp(currentHealthLevel, 0.0f, maxHealthLevel);


        float normalized_health = currentHealthLevel / maxHealthLevel;

        rend.material.Lerp(damagedMaterial, fullHealthMaterial, normalized_health);
        if (healthBar != null)
        { 
            healthBar.HealHealthBar(Mathf.Clamp(heal_amount, 0.0f, maxHealthLevel)); 
        }
    }

    private void InstanceSeveredLimb(Mesh mesh_for_limb)
    {
        GameObject limb_instance = Instantiate(severedLimbPrefab, transform.position, fellaObject.transform.rotation, fellaObject.transform);
        limb_instance.GetComponent<MeshRenderer>().material = damagedMaterial;
        limb_instance.GetComponent<MeshFilter>().mesh = mesh_for_limb;
        limb_instance.GetComponent<MeshCollider>().sharedMesh = mesh_for_limb;
    }
    
    public float GetCurrentHealth()
    {
        return currentHealthLevel;
    }

    public float GetMaxHealth()
    {
        return maxHealthLevel;
    }
    
}
