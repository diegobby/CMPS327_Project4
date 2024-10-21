using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPathToClick : MonoBehaviour
{
    public float moveSpeed = 5.0f;           // Speed of movement
    public float waypointRadius = 0.5f;      // Radius to consider "reached" for waypoints
    public float randomOffsetRange = 2.0f;   // How far the random waypoints can deviate

    private Vector3 targetPosition;          // The final target position (clicked point)
    private bool hasTarget = false;          // Whether the object has a target
    private List<Vector3> waypoints = new List<Vector3>(); // List of random waypoints to follow
    private int currentWaypointIndex = 0;    // Index to track the current waypoint

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();

        if (hasTarget && waypoints.Count > 0)
        {
            MoveAlongRandomPath();
        }
    }

    // Detects mouse click and sets the target position
    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPosition = hit.point;  // Set the clicked point as the target
                GenerateRandomPath();        // Generate random waypoints to the target
                hasTarget = true;
                currentWaypointIndex = 0;    // Start at the first waypoint
            }
        }
    }

    // Generate random waypoints between the current position and the target
    void GenerateRandomPath()
    {
        waypoints.Clear();  // Clear any previous waypoints

        // Determine how many waypoints we want between the current position and target
        int waypointCount = Random.Range(3, 6);  // Random number of waypoints

        Vector3 currentPos = transform.position;

        // Generate each waypoint with a random offset
        for (int i = 0; i < waypointCount; i++)
        {
            // Linearly interpolate between the current position and target
            float t = (i + 1) / (float)(waypointCount + 1);
            Vector3 pointOnLine = Vector3.Lerp(currentPos, targetPosition, t);

            // Add random offset to make the path non-linear
            Vector3 randomOffset = new Vector3(
                Random.Range(-randomOffsetRange, randomOffsetRange),
                0, // Assuming XZ plane movement
                Random.Range(-randomOffsetRange, randomOffsetRange)
            );

            Vector3 randomWaypoint = pointOnLine + randomOffset;
            waypoints.Add(randomWaypoint);
        }

        // Add the final target as the last waypoint
        waypoints.Add(targetPosition);
    }

    // Move along the generated random path (waypoints)
    void MoveAlongRandomPath()
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            hasTarget = false;  // Stop when we have reached the final target
            return;
        }

        Vector3 currentWaypoint = waypoints[currentWaypointIndex];

        // Move towards the current waypoint
        Vector3 direction = (currentWaypoint - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // If we are close enough to the current waypoint, move to the next one
        if (Vector3.Distance(transform.position, currentWaypoint) < waypointRadius)
        {
            currentWaypointIndex++;
        }
    }
}
