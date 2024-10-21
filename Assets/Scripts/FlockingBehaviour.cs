using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlockingAgent : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float minDistance = 1.0f;  // Separation distance
    public float cohesionRadius = 5.0f;  // Cohesion radius
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 1.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Find neighboring agents
        List<GameObject> neighbors = FindNeighbors();

        // Compute the flocking force based on separation, alignment, and cohesion
        Vector3 flockingForce = Flocking(neighbors, minDistance, cohesionRadius, separationWeight, alignmentWeight, cohesionWeight);

        // Apply the flocking force to the agent's velocity
        rb.velocity += flockingForce * Time.deltaTime;
    }

    // Method to find nearby agents (assuming you have a way to identify neighbors)
    List<GameObject> FindNeighbors()
    {
        List<GameObject> neighbors = new List<GameObject>();

        // Example: Find all nearby objects tagged as "Agent"
        foreach (var agent in GameObject.FindGameObjectsWithTag("Agent"))
        {
            if (agent != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, agent.transform.position);
                if (distance < cohesionRadius)
                {
                    neighbors.Add(agent);
                }
            }
        }

        return neighbors;
    }

    // Separation: Avoids getting too close to other agents
    public Vector3 Separation(List<GameObject> neighbors, float minDistance)
    {
        Vector3 separationForce = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            Vector3 toNeighbor = transform.position - neighbor.transform.position;
            if (toNeighbor.magnitude < minDistance)
            {
                separationForce += toNeighbor.normalized / toNeighbor.magnitude;
            }
        }

        return separationForce;
    }

    // Alignment: Aligns with the velocity of nearby agents
    public Vector3 Alignment(List<GameObject> neighbors)
    {
        Vector3 averageDirection = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            averageDirection += neighbor.GetComponent<Rigidbody>().velocity;
        }

        if (neighbors.Count > 0)
        {
            averageDirection /= neighbors.Count;
            averageDirection = averageDirection.normalized;
        }

        return averageDirection;
    }

    // Cohesion: Moves towards the center of mass of nearby agents
    public Vector3 Cohesion(List<GameObject> neighbors, float cohesionRadius)
    {
        Vector3 centerOfMass = Vector3.zero;

        foreach (var neighbor in neighbors)
        {
            centerOfMass += neighbor.transform.position;
        }

        if (neighbors.Count > 0)
        {
            centerOfMass /= neighbors.Count;
            Vector3 toCenter = centerOfMass - transform.position;

            if (toCenter.magnitude > cohesionRadius)
            {
                toCenter = toCenter.normalized;
            }

            return toCenter;
        }

        return Vector3.zero;
    }

    // Flocking: Combines separation, alignment, and cohesion forces
    public Vector3 Flocking(List<GameObject> neighbors, float minDistance, float cohesionRadius, float separationWeight, float alignmentWeight, float cohesionWeight)
    {
        Vector3 separation = Separation(neighbors, minDistance) * separationWeight;
        Vector3 alignment = Alignment(neighbors) * alignmentWeight;
        Vector3 cohesion = Cohesion(neighbors, cohesionRadius) * cohesionWeight;

        // Combine the three behaviors
        Vector3 flockingForce = separation + alignment + cohesion;

        return flockingForce;
    }
}

