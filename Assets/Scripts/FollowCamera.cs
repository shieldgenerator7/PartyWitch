using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Expose variable to editor
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 CameraOffsetDistance;

    [SerializeField] [Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f;

    private Vector3 velocity = Vector3.zero;

    // Called after all the updates
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + CameraOffsetDistance;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
