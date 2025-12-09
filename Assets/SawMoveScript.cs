using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMoveScript : MonoBehaviour
{

public float moveSpeed = 2f;
public float moveDistance = 3f;
public bool startMovingRight = true;

private Vector3 startPosition;
private bool movingRight;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        movingRight = startMovingRight;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = startPosition + (movingRight ? Vector3.forward : Vector3.back) * moveDistance;

        transform.position = Vector3.MoveTowards(current: transform.position, targetPosition, maxDistanceDelta: moveSpeed * Time.deltaTime);

        if (Vector3.Distance(a: transform.position, b:targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }

    }
}
