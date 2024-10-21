using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Transform target; // Target to flee from
    public float moveSpeed = 6.0f; // Speed of movement
    private float maxDistance = 5.0f; // Distance threshold for fleeing
    private float planeHeight = 0.0f; // Height of the ground plane (adjust as necessary)

    // Update is called once per frame
    void Update()
    {
        FleeTarget(); // Call the fleeing function every frame
    }

    void FleeTarget()
    {
        // Check if the target is assigned
        if (target == null)
        {
            Debug.Log("Target not assigned.");
            return; // If no target is assigned, do nothing
        }

        // Calculate the direction vector from the target to this object
        Vector3 dir = target.position - transform.position;

        // Check if the target is within the fleeing range
        if (dir.magnitude < maxDistance)
        {
            // Calculate the move vector to flee
            Vector3 moveVector = dir.normalized * moveSpeed * Time.deltaTime;

            // Move in the opposite direction of the target
            transform.position -= moveVector;

            // Keep the fleeer on the plane by setting the y position to the plane height
            transform.position = new Vector3(transform.position.x, planeHeight, transform.position.z);

            // Optionally rotate the object to face away from the target
            Quaternion fleeRotation = Quaternion.LookRotation(-dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, fleeRotation, 0.1f); // Smooth rotation
        }
    }
}
