using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public DraggableBody ropeSegment;
    //public DraggableBody connectedBody;
    public DraggableBody[] connectedBodies;

    public float lengthOfRope = 3.0f;
    void Start()
    {
        SetRopeLength();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRopeLength()
    {
        float individual_length = lengthOfRope / (float)connectedBodies.Length;//Replace with dynamic number of rope segments

        foreach (DraggableBody i in connectedBodies)
        {
            i.distanceLimit1 = individual_length;
            i.distanceLimit2 = individual_length;

        }
        
        
    }
}
