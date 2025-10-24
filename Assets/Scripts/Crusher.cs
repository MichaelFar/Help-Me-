using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Crusher : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PointA;
    public Transform PointB;
    public Transform movementParent;


    public float timeAtTop = 3.0f;
    public float timeAtBottom = 2.0f;
    public float timeToRise = 1.0f;
    public float timeToFall = 0.1f;

    private float timer = 0.0f;

    private enum CrusherState {AT_TOP, AT_BOTTOM, RISING, FALLING};

    private CrusherState currentState = CrusherState.AT_TOP;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CrusherStateManager();

    }

    void CrusherStateManager()
    {
        float timer_target = 0.0f;
        if(currentState == CrusherState.AT_BOTTOM)
        {
            timer_target = timeAtBottom;
            if(timer >= timer_target)
            {
                timer = 0.0f;
                currentState = CrusherState.RISING;
            }
            //movementParent.transform.position = Vector3.Lerp(PointB.position, PointA.position, timer / timeAtBottom);
        }
        else if(currentState == CrusherState.AT_TOP)
        {
            timer_target = timeAtTop;
            if (timer >= timer_target)
            {
                timer = 0.0f;
                currentState = CrusherState.FALLING;
            }
            //movementParent.transform.position = Vector3.Lerp(PointA.position, PointB.position, timer / timeAtBottom);
        }
        else if (currentState == CrusherState.RISING)
        {
            timer_target = timeToRise;
            if (timer >= timer_target)
            {
                timer = 0.0f;
                currentState = CrusherState.AT_TOP;
            }
            else
            {
                movementParent.transform.position = Vector3.Lerp(PointB.position, PointA.position, timer / timeToRise);
            }
                
        }
        else if (currentState == CrusherState.FALLING)
        {
            timer_target = timeToFall;
            if (timer >= timer_target)
            {
                timer = 0.0f;
                currentState = CrusherState.AT_BOTTOM;
            }
            else
            {
                movementParent.transform.position = Vector3.Lerp(PointA.position, PointB.position, timer / timeToFall);
            }
                
        }


    }
}
