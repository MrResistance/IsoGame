using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.7f;
    public float rotateMagnitude = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private IEnumerator Shake(float duration, float magnitude, float rotationMagnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.localPosition = initialPosition + new Vector3 (Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0) * magnitude;

            Quaternion randomRotation = new Quaternion(0, 0,
                Random.Range(-rotationMagnitude, rotationMagnitude),
                1).normalized;

            transform.localRotation = initialRotation * randomRotation;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
    }

    public void TriggerShake(float duration, float magnitude, float rotationMagnitude)
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;

        StartCoroutine(Shake(duration, magnitude, rotationMagnitude));
    }
}
