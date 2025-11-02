using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public DraggableBody ropeSegment;
    //public DraggableBody connectedBody;
    public DraggableBody[] connectedBodies;
    public GameObject ropeAnchor;
    public DraggableBody bodyConnection;
    public float lengthOfRope = 3.0f;
    public float ropeMass = 1.0f;
    public float ropeForce = 1.0f;
    public float distanceCoefficient = 1.3f;
    public float ropeDrag = 1.0f;
    void Start()
    {
        SetRopeLength();
        SetRopeForce();
        SetRopeMass();
        SetDistanceCoefficient();
        SetRopeDrag();
        LinkBodies();
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
    void SetRopeForce()
    {
        //float individual_length = lengthOfRope / (float)connectedBodies.Length;//Replace with dynamic number of rope segments

        foreach (DraggableBody i in connectedBodies)
        {
            i.dragForce = ropeForce;
            //i.distanceLimit2 = individual_length;

        }
    }
    void SetRopeMass()
    {
        foreach (DraggableBody i in connectedBodies)
        {
            i.GetComponent<Rigidbody>().mass = ropeMass;
            //i.distanceLimit2 = individual_length;

        }
    }
    void SetDistanceCoefficient()
    {
        foreach (DraggableBody i in connectedBodies)
        {
            i.GetComponent<DraggableBody>().distanceCoefficient = distanceCoefficient;
            //i.distanceLimit2 = individual_length;

        }
    }

    void SetRopeDrag()
    {
        foreach (DraggableBody i in connectedBodies)
        {
            i.GetComponent<Rigidbody>().drag = ropeDrag;
            //i.distanceLimit2 = individual_length;

        }
    }

    void LinkBodies()
    {

        for (int i = 0; i < connectedBodies.Length; i++)
        {
            if (i - 1 < 0)
            {
                connectedBodies[i].destinationObject1 = ropeAnchor.transform;
                //connectedBodies[i].distanceLimit1 = (lengthOfRope / (float)connectedBodies.Length) / 2;

            }
            else
            {
                connectedBodies[i].destinationObject1 = connectedBodies[i - 1].transform;
            }


            if (i + 1 < connectedBodies.Length)
            {
                connectedBodies[i].destinationObject2 = connectedBodies[i + 1].transform;
            }
            else
            {
                connectedBodies[i].destinationObject2 = bodyConnection.transform;
            }
        }
    }
}
