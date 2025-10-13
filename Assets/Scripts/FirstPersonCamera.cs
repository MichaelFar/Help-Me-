using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Vector2 sensitivityVector = Vector2.one;

    public Transform playerTransform;

    private Vector2 rotationVector = Vector2.zero;

    

    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityVector.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityVector.y;

        Vector2 mouseVector = new Vector2(mouseX, mouseY);

        rotationVector.y += mouseVector.x;

        rotationVector.x -= mouseVector.y;

        rotationVector.x = Mathf.Clamp(rotationVector.x, -90f, 90f);

        //transform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);
        playerTransform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);//Quaternion.Euler(rot, rotationVector.y, 0);


    }
}
