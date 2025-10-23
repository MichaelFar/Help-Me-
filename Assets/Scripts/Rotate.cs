using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.0f;
    public bool shouldSpin = true;
    public float direction = 1.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpin)
        {
            Vector3 rotation_vector = new Vector3(gameObject.transform.localRotation.x, (gameObject.transform.localRotation.y + (speed * direction)) * Time.deltaTime, gameObject.transform.localRotation.z);
            gameObject.transform.Rotate(rotation_vector);
        }
    }
}
