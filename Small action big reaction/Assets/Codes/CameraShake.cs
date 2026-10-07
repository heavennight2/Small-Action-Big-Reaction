using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private bool continuousShake = false;
    private float continuousStrength = 0f;

    private void Awake()
    {
        instance = this;
    }

    // الشيك القوي المؤقت
    public void StartShake(float duration, float strength)
    {
        StartCoroutine(Shake(duration, strength));
    }

    private IEnumerator Shake(float duration, float strength)
    {
        float timer = 0f;

        while (timer < duration)
        {
            Vector3 offset = new Vector3(
                Random.Range(-strength, strength),
                Random.Range(-strength, strength),
                0f
            );

            transform.position += offset;

            timer += Time.deltaTime;

            yield return null;
        }
    }

    // الشيك الخفيف المستمر
    public void StartContinuousShake(float strength)
    {
        continuousStrength = strength;
        continuousShake = true;
    }

    public void StopShake()
    {
        continuousShake = false;
        continuousStrength = 0f;
    }

    private void LateUpdate()
    {
        if (continuousShake)
        {
            Vector3 offset = new Vector3(
                Random.Range(-continuousStrength, continuousStrength),
                Random.Range(-continuousStrength, continuousStrength),
                0f
            );

            transform.position += offset;
        }
    }
}