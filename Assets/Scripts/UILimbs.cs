using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UILimbs : MonoBehaviour
{


    public CollisionDamageLerper thisLimb;
    public UnityEngine.UI.Image thisImage;
    public Material damagedMaterial;

    private bool hasDied = false;
    private bool hasDiedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        thisImage.color = thisLimb.rend.material.color;
        if(hasDied && !hasDiedOnce)
        {
            thisImage.color = damagedMaterial.color;
            hasDied = true;
        }
        if (!hasDiedOnce)
        {
            hasDied = thisLimb.isDead;
        }
    }
}
