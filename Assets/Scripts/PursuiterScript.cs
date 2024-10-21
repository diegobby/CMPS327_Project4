using UnityEngine;
public class MovingTarget : MonoBehaviour
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
        SeekTarget();
    }

    void UpdateTargetPosition()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
        {
            target = hit.point;
        }
    }

    void SeekTarget()
    {
        Vector3 dir = target - transform.position;
        if (dir.magnitude < 0.005f) dir = Vector3.zero;
        
        // Calculate move distance
        float step = moveSpeed * Time.deltaTime;

        // Move towards target without overshooting
        if (dir.magnitude > step)
        {
            velocity = dir.normalized * moveSpeed;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            // Directly move to the target
            transform.position = target;
        }
    }
}
