using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float rotationSpeed = 12f;
    public float maxRotation = 35f;

    private bool movementStarted = false;
    private float direction = 1f;

    void Update()
    {
        if (!movementStarted)
            return;

        // الزاوية الحالية
        float angle = transform.eulerAngles.z;

        if (angle > 180f)
            angle -= 360f;

        // إذا وصل اليمين، ارجع
        if (angle >= maxRotation)
            direction = -1f;

        // إذا وصل اليسار، ارجع
        else if (angle <= -maxRotation)
            direction = 1f;

        // دوران مستمر وناعم
        float newAngle =
            angle + direction * rotationSpeed * Time.deltaTime;

        transform.rotation =
            Quaternion.Euler(0f, 0f, newAngle);
    }

    public void StartMovement()
    {
        movementStarted = true;
        direction = 1f;
    }
}