using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 50.0f;
    public ThirdPersonCameraParent cameraParent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x_axis = Input.GetAxis("Horizontal");
        float z_axis = Input.GetAxis("Vertical");

        float rise_axis = Input.GetAxis("Rise");

        Vector3 move_direction = (transform.right * x_axis) + (transform.forward * z_axis) + (transform.up * rise_axis);
        move_direction = move_direction.normalized;
        if(move_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, cameraParent.rotationVector.y, 0);

            controller.Move(move_direction * speed * Time.deltaTime);
        }
            
    }
}
