using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 50.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move_direction = (transform.right * x) + (transform.forward * z);
        move_direction = move_direction.normalized;

        controller.Move(move_direction * speed * Time.deltaTime);
    }
}
