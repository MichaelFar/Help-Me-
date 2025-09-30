using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBody : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;
    public Transform destinationObject;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(destinationObject.position);
    }
}
