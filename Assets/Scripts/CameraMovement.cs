using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}