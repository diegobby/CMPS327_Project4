using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5.0f;
    private float minDistance = 0.05f;

    // Update is called once per frame
    void Update()
    {
        SeekTarget();
    }

    void SeekTarget()
    {
        // Check if the target is set
        if (target == null)
        {
            return; // If no target is assigned, do nothing
        }

        // Move towards the target position
        float step = moveSpeed * Time.deltaTime; // Calculate distance to move

        // Check if we are already at the desired distance
        if (Vector3.Distance(transform.position, target.position) > minDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Optionally rotate to face the target
            Vector3 direction = target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
