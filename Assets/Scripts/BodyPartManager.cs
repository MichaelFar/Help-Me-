using System;
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

    public GameObject severedLimbObject;

    public ScoreTracker scoreTracker;

    public List<CollisionDamageLerper> ListOfLimbLerpers = new List<CollisionDamageLerper>();

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
            scoreTracker.CalculateScore();
            scoreTracker.SetScoreLabelText();
            deathScreen.SetShown(true);
        }
    }

    void SetLimbProperties()
    {
        DraggableBody[] limb_array = {head, torso, leftArm, rightArm, leftLeg, rightLeg};

        List<CollisionDamageLerper> limb_lerpers = new List<CollisionDamageLerper>();

        foreach (DraggableBody i in limb_array)
        {
            CollisionDamageLerper health_object = i.GetComponent<CollisionDamageLerper>();
            health_object.canBeSevered = true;
            health_object.bodyPartManager = this;
            health_object.severedLimbPrefab = severedLimbObject;
            Rigidbody rigidBody = i.GetComponent<Rigidbody>();
            limb_lerpers.Add(health_object);
            if(i == head)
            {
                headDamageObject = health_object;
                scoreTracker.headObject = headDamageObject;
                health_object.maxHealthLevel = headHealth;
                health_object.forceThreshold = headMinumumForceThreshold;
                rigidBody.mass = headMass;
                rigidBody.drag = headDrag;
            }
            else if(i == torso)
            {
                health_object.maxHealthLevel = torsoHealth;
                health_object.forceThreshold = torsoMinumumForceThreshold;
                //health_object.canBeSevered = false;
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
            health_object.fellaObject = headDamageObject.gameObject;

        }
        ListOfLimbLerpers = limb_lerpers;
        healthBar.PopulateHealth();
        scoreTracker.PopulateHealthLerpers(limb_array);
    }

    public void SetLimbDestroyedProperties(Mesh limb_mesh)
    {
        DraggableBody[] limb_array = { head, torso, leftArm, rightArm, leftLeg, rightLeg };
        foreach (DraggableBody i in limb_array)
        {
            CollisionDamageLerper this_lerper = i.GetComponent<CollisionDamageLerper>();
            //this_lerper.isDead = true;
            GameObject limb_object = this_lerper.rend.gameObject;
            GameObject limb_physics_object = i.gameObject;

            if (limb_object.GetComponent<SkinnedMeshRenderer>().sharedMesh == limb_mesh)
            {
                //limb_object.layer = ""
                this_lerper.isDead = true;
                Rigidbody this_rigid_body = GetRigidBodyOfLimb(i);
                this_rigid_body.mass = 0.1f;
                Collider potential_collider = limb_object.GetComponent<Collider>();
                if(potential_collider)
                {
                    potential_collider.isTrigger = true;
                }
                //this_rigid_body.drag = 0.0f;
                //Destroy(limb_physics_object.GetComponent<Collider>());

            }
        }
    }
    public GameObject GetLimbObjectFromMesh(Mesh limb_mesh)
    {
        DraggableBody[] limb_array = {head, torso, leftArm, rightArm, leftLeg, rightLeg};
        foreach (DraggableBody i in limb_array)
        {
            CollisionDamageLerper this_lerper = i.GetComponent<CollisionDamageLerper>();
            //this_lerper.isDead = true;
            GameObject limb_object = this_lerper.rend.gameObject;
            if (limb_object.GetComponent<SkinnedMeshRenderer>().sharedMesh == limb_mesh)
            {
                return limb_object;
            }
        }
        return null;
    }
    public CollisionDamageLerper GetLerperFromMesh(Mesh limb_mesh)
    {
        DraggableBody[] limb_array = { head, torso, leftArm, rightArm, leftLeg, rightLeg };
        foreach (DraggableBody i in limb_array)
        {
            CollisionDamageLerper this_lerper = i.GetComponent<CollisionDamageLerper>();
            //this_lerper.isDead = true;
            GameObject limb_object = this_lerper.rend.gameObject;
            if (limb_object.GetComponent<SkinnedMeshRenderer>().sharedMesh == limb_mesh)
            {
                return this_lerper;
            }
        }
        return null;
    }
    public Rigidbody GetRigidBodyOfLimb(DraggableBody body)
    {
        return body.GetComponent<Rigidbody>();
    }
}
