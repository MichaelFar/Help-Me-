using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDamageValue : MonoBehaviour
{
    // Start is called before the first frame update

    public float customDamageOnHit = 5.0f;
    

    public float GetDamageOnHit()
    {
        return customDamageOnHit;
    }
}
