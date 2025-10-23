using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    
    public CollisionDamageLerper damageLerper;
    public Transform destinationObject1;
    public Transform destinationObject2;
    public float distanceLimit1 = 5.0f;
    public float distanceLimit2 = 5.0f;
    public float dragForce = 3.0f;

    private float collisionDamageCoolDownCounter = 0.0f;
    

    //Proto Health System, will be changed for per material and body part
    //private float healthLevel = 0.0f; //Most healthy at 0.0, might change logic to reverse if unintuitive
    public float damageFromImpact = 0.25f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        collisionDamageCoolDownCounter += Time.deltaTime;
        if (destinationObject1 != null)
        {
            float distance_to_object1 = Vector3.Distance(transform.position, destinationObject1.position);
            Vector3 direction_to_object1 = destinationObject1.position - transform.position;
            float direction_ratio1 = 1.0f - (distanceLimit1 / distance_to_object1);// Remember this for future implementations

            direction_ratio1 = Mathf.Clamp(distanceLimit1, distanceLimit1, direction_ratio1 * 1.3f);//Remember this for future implementations
            direction_to_object1 = direction_to_object1.normalized;

            rb.AddForce(direction_to_object1 * dragForce * direction_ratio1);// * direction_ratio1);// * distance_to_object1);

            
        }
        if (destinationObject2 != null)
        {
            float distance_to_object2 = Vector3.Distance(transform.position, destinationObject2.position);
            Vector3 direction_to_object2 = destinationObject2.position - transform.position;

            float direction_ratio2 = 1.0f - (distanceLimit2 / distance_to_object2);

            direction_ratio2 = Mathf.Clamp(distanceLimit2, distanceLimit2, direction_ratio2 * 1.3f);
            print("Direction ratio2 is " + direction_ratio2);
            direction_to_object2 = direction_to_object2.normalized;

            rb.AddForce(direction_to_object2 * dragForce * direction_ratio2);// * direction_ratio2);// * distance_to_object2);
                
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Testing collision on rigid body");
        print("Collided with " + collision.gameObject);
        if(collision.gameObject.tag != "DONOTTAKEDAMAGE" && collisionDamageCoolDownCounter > 1.0f)
        {
            if(collision.gameObject.tag == "INSTANTKILL")
            {
                print("Limb instant killed");
                collisionDamageCoolDownCounter = 0.0f;
                damageLerper.DamageMesh(1000000000.0f);
            }
            else
            {
                collisionDamageCoolDownCounter = 0.0f;
                damageLerper.DamageMesh(GetMagnitudeOfCollison());
            }
                
        }

    }
    private float GetMagnitudeOfCollison()
    {
        Vector3 force = (rb.mass * rb.velocity) / Time.deltaTime;
        print("Magnitude of collision is " + force.magnitude);
        return force.magnitude;
    }

}
