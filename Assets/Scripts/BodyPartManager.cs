using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    // Start is called before the first frame update
    private CollisionDamageLerper headDamageObject;

    public HealthBar healthBar;
    [Header("Limb References")]
    public DraggableBody head;
    public DraggableBody torso;
    public DraggableBody rightArm;
    public DraggableBody leftArm;
    public DraggableBody rightLeg;
    public DraggableBody leftLeg;

    [Header("Head Stats")]
    public float headHealth = 100.0f;
    public float headMinumumForceThreshold = 3.0f;
    public float headMass = 0.1f;
    public float headDrag = 1.0f;

    [Header("Torso Stats")]
    public float torsoHealth = 100.0f;
    public float torsoMinumumForceThreshold = 3.0f;
    public float torsoMass = 0.1f;
    public float torsoDrag = 1.0f;

    [Header("Left Arm Stats")]
    public float leftArmHealth = 100.0f;
    public float leftArmMinumumForceThreshold = 3.0f;
    public float leftArmMass = 0.1f;
    public float leftArmDrag = 1.0f;

    [Header("Right Arm Stats")]
    public float rightArmHealth = 100.0f;
    public float rightArmMinumumForceThreshold = 3.0f;
    public float rightArmMass = 0.1f;
    public float rightArmDrag = 1.0f;

    [Header("Left Leg Stats")]
    public float leftLegHealth = 100.0f;
    public float leftLegMinumumForceThreshold = 3.0f;
    public float leftLegMass = 0.1f;
    public float leftLegDrag = 1.0f;

    [Header("Right Leg Stats")]
    public float rightLegHealth = 100.0f;
    public float rightLegMinumumForceThreshold = 3.0f;
    public float rightLegMass = 0.1f;
    public float rightLegDrag = 1.0f;

    public DeathVictoryButtons deathScreen;

    private int numDied = 0;
    void Start()
    {
        SetLimbProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if(headDamageObject.isDead && numDied < 1)
        {
            print("Head has died and you lose");
            numDied += 1;
            ///Put death function here 
            deathScreen.SetShown(true);
        }
    }

    void SetLimbProperties()
    {
        DraggableBody[] limb_array = {head, torso, leftArm, rightArm, leftLeg, rightLeg};
        
        foreach (DraggableBody i in limb_array)
        {
            CollisionDamageLerper health_object = i.GetComponent<CollisionDamageLerper>();
            Rigidbody rigidBody = i.GetComponent<Rigidbody>();
            if(i == head)
            {
                headDamageObject = health_object;
                health_object.maxHealthLevel = headHealth;
                health_object.forceThreshold = headMinumumForceThreshold;
                rigidBody.mass = headMass;
                rigidBody.drag = headDrag;
            }
            else if(i == torso)
            {
                health_object.maxHealthLevel = torsoHealth;
                health_object.forceThreshold = torsoMinumumForceThreshold;
                rigidBody.mass = torsoMass;
                rigidBody.drag = torsoDrag;
            }
            else if (i == leftArm)
            {
                health_object.maxHealthLevel = leftArmHealth;
                health_object.forceThreshold = leftArmMinumumForceThreshold;
                rigidBody.mass = leftArmMass;
                rigidBody.drag = leftArmDrag;
            }
            else if (i == rightArm)
            {
                health_object.maxHealthLevel = rightArmHealth;
                health_object.forceThreshold = rightArmMinumumForceThreshold;
                rigidBody.mass = rightArmMass;
                rigidBody.drag = rightArmDrag;
            }
            else if (i == leftLeg)
            {
                health_object.maxHealthLevel = leftLegHealth;
                health_object.forceThreshold = leftLegMinumumForceThreshold;
                rigidBody.mass = leftLegMass;
                rigidBody.drag = leftLegDrag;
            }
            else if (i == rightLeg)
            {
                health_object.maxHealthLevel = rightLegHealth;
                health_object.forceThreshold = rightLegMinumumForceThreshold;
                rigidBody.mass = rightLegMass;
                rigidBody.drag = rightLegDrag;
            }

        }

        healthBar.PopulateHealth();
        
        
        
        //Assign Health
        
    }
}
