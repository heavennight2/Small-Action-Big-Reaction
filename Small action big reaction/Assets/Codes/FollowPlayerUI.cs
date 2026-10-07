using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(
            player.position + offset
        );

        transform.position = screenPosition;
    }
}