using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    private float cameraZ;

    void Start()
    {
        cameraZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            cameraZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}