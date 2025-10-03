using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    public Transform destinationObject;
    public float distanceLimit = 5.0f;
    public float dragForce = 3.0f;

    public Material damagedMaterial;
    public Material fullHealthMaterial;
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance_to_object = Vector3.Distance(transform.position, destinationObject.position);
        Vector3 direction_to_object = destinationObject.position - transform.position;

        direction_to_object = direction_to_object.normalized;

        if (distance_to_object > distanceLimit)
        {

            rb.AddForce(direction_to_object * dragForce);
            //rb.MovePosition(destinationObject.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Testing collision on rigid body");
    }
}
