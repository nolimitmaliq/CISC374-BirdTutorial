using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float swayAmount = 0.2f; // Reduced vertical sway

    void Update()
    {
        // Horizontal movement with slight vertical variation
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Sin(Time.time) * swayAmount + transform.position.y,
            transform.position.z
        );

        if (transform.position.x < -15f)
            Destroy(gameObject);
    }
}