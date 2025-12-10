using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForceApplyer : MonoBehaviour
{
    // Start is called before the first frame update
    public float forceMagnitude = 50.0f;

    public float forceCooldownSec = 0.3f;

    public Transform parentTransform;

    private float timer = 0.0f;

    private List<DraggableBody> draggablesInVolume = new List<DraggableBody>();
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        foreach (DraggableBody i in draggablesInVolume)
        {
            ApplyForceToDraggable(i);

        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        bool is_draggable = other.gameObject.GetComponent<DraggableBody>();
        if (is_draggable)
        {
            //print("Adding draggable to list");
            DraggableBody new_draggable = other.gameObject.GetComponent<DraggableBody>();
            draggablesInVolume.Add(new_draggable);
           // print(" Adding to list: " + draggablesInVolume);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bool is_draggable = other.gameObject.GetComponent<DraggableBody>();
        if (is_draggable)
        {
            //print("Removing draggable from list");
            DraggableBody new_draggable = other.gameObject.GetComponent<DraggableBody>();
            draggablesInVolume.Remove(new_draggable);
            //print(" Removing from list: " + draggablesInVolume);
        }
    }
    private void ApplyForceToDraggable(DraggableBody draggable)
    {
        Vector3 direction_to_fan = transform.position - parentTransform.position;
        direction_to_fan = direction_to_fan.normalized;
        draggable.GetComponent<Rigidbody>().AddForce(direction_to_fan * forceMagnitude * Time.deltaTime);
        //print("Applying force to " + draggable.gameObject);
    }
}
