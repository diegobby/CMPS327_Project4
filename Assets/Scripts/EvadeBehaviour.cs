using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class Evade : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0);
    public float moveSpeed = 6.0f;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTargetPosition();
        }
        EvadeTargetPosition();
    }

    void UpdateTargetPosition()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            target = hit.point;
        }
    }

    void EvadeTargetPosition()
    {
        // Calculate direction to target
        Vector3 dirToTarget = target - transform.position;

        // If we're close to the target, don't move
        if (dirToTarget.magnitude < 0.005f)
        {
            return;
        }

        // Invert direction to move away from the target
        Vector3 moveDirection = -dirToTarget.normalized * moveSpeed;

        // Move the object
        velocity = moveDirection;
        transform.position += velocity * Time.deltaTime;
    }
}
