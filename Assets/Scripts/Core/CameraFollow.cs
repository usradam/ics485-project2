using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance = 2f;
    [SerializeField] private float cameraSpeed = 5f;
    [SerializeField] private float smoothTime = 0.25f;

    private float lookAhead;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        // SmoothDamp will handle the transition

    }
}
