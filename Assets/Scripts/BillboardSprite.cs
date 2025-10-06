using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera currentCamera;
    public Transform playerTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FaceCamera();
    }//

    void FaceCamera()
    {

        transform.forward = currentCamera.transform.forward;
        //transform.rotation = Quaternion.Euler(currentCamera.transform.rotation.x, transform.rotation.y, 0);
        //transform.rotation = Quaternion.Euler(playerTransform.rotation.x, playerTransform.rotation.y, playerTransform.rotation.z);
    }
}
