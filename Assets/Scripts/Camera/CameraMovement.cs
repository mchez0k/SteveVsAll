using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        transform.position = (player.transform.position + offset);
    }
}
