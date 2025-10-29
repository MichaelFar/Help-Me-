using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointVolume : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool has_reached = false;
    public bool hasReached = false;
            

    public CheckPointManager myManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bool is_player = other.gameObject.GetComponent<FPCharacterController>();
        if (is_player)
        {
            print("Player went through volume");
            hasReached = true;
            myManager.CheckTheList();
        }
        
    }
}
