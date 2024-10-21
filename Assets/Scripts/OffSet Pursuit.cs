using UnityEngine;

public class MovingTargetWithDistance : MonoBehaviour
{
    Vector3 target = new Vector3(0, 0, 0);
    public float moveSpeed = 6.0f;
    public float minDistance = 2.0f; // Minimum distance to keep from the target
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
        SeekTarget();
    }

    void UpdateTargetPosition()
    {
        // Update target to the clicked point
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            target = hit.point;
        }
    }

    void SeekTarget()
    {
        // Calculate the direction to the target
        Vector3 dir = target - transform.position;
        float distanceToTarget = dir.magnitude;

        // If the object is within the minimum distance, stop moving
        if (distanceToTarget <= minDistance)
        {
            velocity = Vector3.zero;
            return;
        }

        // Move towards the target while maintaining a distance
        Vector3 moveDirection = dir.normalized * moveSpeed;
        velocity = moveDirection;
        transform.position += velocity * Time.deltaTime;
    }
}
