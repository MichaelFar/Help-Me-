using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealVolume : MonoBehaviour
{
    // Start is called before the first frame update

    public float healAmount = 100.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bool is_draggable = other.gameObject.GetComponent<DraggableBody>();

        if(is_draggable)
        {
            CollisionDamageLerper damage_lerper = other.gameObject.GetComponent<CollisionDamageLerper>();
            if (other.gameObject.GetComponent<CollisionDamageLerper>())
            {
                damage_lerper.HealMesh(healAmount);
            }
        }
    }
}
