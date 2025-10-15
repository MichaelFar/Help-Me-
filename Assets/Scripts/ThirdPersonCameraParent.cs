using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraParent : MonoBehaviour
{
    public Vector2 sensitivityVector = Vector2.one;

    public Transform playerTransform;

    public Vector2 rotationVector = Vector2.zero;


    public Camera playerCamera;
    public Transform nearCameraLimit;
    public Transform farCameraLimit;
    public float zoomInterpolateCoeefficient = 0.1f;
    private float zoomRatio = 0.0f;

    // Start is called before the first frame update

    private void Start()
    {
        ZoomCamera(Vector2.one);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityVector.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityVector.y;

        Vector2 mouseVector = new Vector2(mouseX, mouseY);

        rotationVector.y += mouseVector.x;

        rotationVector.x -= mouseVector.y;

        rotationVector.x = Mathf.Clamp(rotationVector.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);
        transform.position = playerTransform.position;
        //playerTransform.rotation = Quaternion.Euler(rotationVector.x, rotationVector.y, 0);//Quaternion.Euler(rot, rotationVector.y, 0);

        if(Input.mouseScrollDelta != Vector2.zero)
        {
            ZoomCamera(Input.mouseScrollDelta);
        }
    }
    public void ZoomCamera(Vector2 new_vector)
    {
        zoomRatio += new_vector.y * zoomInterpolateCoeefficient;
        zoomRatio = Mathf.Clamp(zoomRatio, 0.0f, 1.0f);
        playerCamera.transform.position = Vector3.Lerp(farCameraLimit.transform.position, nearCameraLimit.transform.position, zoomRatio);
        print("PlayerScrolled: " + new_vector);
    }
}
