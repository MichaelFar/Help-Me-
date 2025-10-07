using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    public Renderer rend;
    public CollisionDamageLerper damageLerper;
    public Transform destinationObject1;
    public Transform destinationObject2;
    public float distanceLimit1 = 5.0f;
    public float distanceLimit2 = 5.0f;
    public float dragForce = 3.0f;

    

    //Proto Health System, will be changed for per material and body part
    //private float healthLevel = 0.0f; //Most healthy at 0.0, might change logic to reverse if unintuitive
    public float damageFromImpact = 0.25f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance_to_object1 = Vector3.Distance(transform.position, destinationObject1.position);
        Vector3 direction_to_object1 = destinationObject1.position - transform.position;

        

        direction_to_object1 = direction_to_object1.normalized;
        
        

        if (distance_to_object1 > distanceLimit1)
        {

            rb.AddForce(direction_to_object1 * dragForce * distance_to_object1);
            
            //rb.MovePosition(destinationObject.position);
        }

        if (destinationObject2 != null)
        {
            float distance_to_object2 = Vector3.Distance(transform.position, destinationObject2.position);
            Vector3 direction_to_object2 = destinationObject2.position - transform.position;

            direction_to_object2 = direction_to_object2.normalized;

            if (distance_to_object2 > distanceLimit2)
            {

                rb.AddForce(direction_to_object2 * dragForce * distance_to_object2);
                //rb.MovePosition(destinationObject.position);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Testing collision on rigid body");
        damageLerper.DamageMesh(GetMagnitudeOfCollison());

    }
    private float GetMagnitudeOfCollison()
    {
        Vector3 force = (rb.mass * rb.velocity) / Time.deltaTime;
        print("Magnitude of collision is " + force.magnitude);
        return force.magnitude;
    }

}
