using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDamageValue : MonoBehaviour
{
    // Start is called before the first frame update

    public float customDamageOnHit = 5.0f;
    public float minimumForceThreshold = 0.0f;
    public bool onlyWhenMoving = false;
    private bool positionChanged = false;
    private Vector3 positionLastFrame = Vector3.zero;
    private Vector3 positionThisFrame = Vector3.zero;

    private int frameCounter = 0;
    public float GetDamageOnHit()
    {

        if(onlyWhenMoving)
        {
            if(positionChanged)
            {
                print("Moving custom damage");
                return customDamageOnHit;
            }
        }
        

            return 0.0f;
    }
    private void Update()
    {
        
        frameCounter += 1;

        if (frameCounter % 2 == 0)
        {
            positionLastFrame = transform.position;
        }
        else
        {
            positionThisFrame = transform.position;
        }
        positionChanged = positionLastFrame == positionThisFrame;
        //print("Position changed is " + positionChanged);
    }
}
