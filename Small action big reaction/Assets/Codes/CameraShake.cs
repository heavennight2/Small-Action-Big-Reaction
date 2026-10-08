
using UnityEngine;

[DefaultExecutionOrder(100)]
public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private bool continuousShake = false;
    private float continuousStrength = 0f;

    private float shakeTimer = 0f;
    private float shakeStrength = 0f;

    private Vector3 lastOffset = Vector3.zero;

    private void Awake()
    {
        instance = this;
    }

    public void StartShake(float duration, float strength)
    {
        shakeTimer = duration;
        shakeStrength = strength;
    }

    public void StartContinuousShake(float strength)
    {
        continuousStrength = strength;
        continuousShake = true;
    }

    public void StopShake()
    {
        continuousShake = false;
        continuousStrength = 0f;
        shakeTimer = 0f;
        shakeStrength = 0f;

        transform.position -= lastOffset;
        lastOffset = Vector3.zero;
    }

    private void LateUpdate()
    {
        // Remove previous frame's shake
        transform.position -= lastOffset;
        lastOffset = Vector3.zero;

        float strength = continuousShake ? continuousStrength : 0f;

        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            strength += shakeStrength;
        }

        if (strength > 0f)
        {
            lastOffset = new Vector3(
                Random.Range(-strength, strength),
                Random.Range(-strength, strength),
                0f
            );

            transform.position += lastOffset;
        }
    }
}
