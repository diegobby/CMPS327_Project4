using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public float speed = 5f; // Adjust speed to control how fast the object moves

    private Vector3 targetPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position; // Start at the object's initial position
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTargetPosition();
        }

        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Stop moving when the object reaches the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    void UpdateTargetPosition()
    {
        // Perform the raycast to detect the clicked point on the ground
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            targetPosition = hit.point; // Set the new target position
            isMoving = true; // Start moving towards the target
        }
    }
}
