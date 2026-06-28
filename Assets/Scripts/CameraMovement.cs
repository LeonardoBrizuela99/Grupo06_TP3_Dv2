using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    private const float posZ = -10;
    private float smoothTime = 0.3f;

    private Vector3 offset = new Vector3(0, 0, posZ);
    private Vector3 speed = Vector3.zero;

    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, posZ);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;

        Vector3 finalPos = Vector3.SmoothDamp(transform.position, targetPosition, ref speed, smoothTime);

        //finalPos.z = posZ;

        transform.position = finalPos;
    }
}
