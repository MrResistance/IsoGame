using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    [Header("References"), SerializeField] private Camera m_camera;
    [Header("Values"), SerializeField] private float m_speed = 1f;
    [Header("Zoom"), SerializeField] private float m_zoomSpeed = 1f;
    [SerializeField] private float m_maxZoomIn;
    [SerializeField] private float m_maxZoomOut;
    [SerializeField] private float m_defaultZoom;

    private void LateUpdate()
    {
        MoveCamera(PlayerInputs.Instance.MoveInput);
        Zoom(PlayerInputs.Instance.ZoomInput);
    }

    private void MoveCamera(Vector2 newVal)
    {
        transform.Translate(newVal * m_speed * Time.deltaTime);
    }

    private void Zoom(float newVal)
    {
        // Determine the zoom direction based on the newVal sign (+ for zooming out, - for zooming in)
        float zoomDelta = m_zoomSpeed * newVal; // You should define zoomSpeed to control how fast the zoom is.

        // Apply the zoomDelta to the current orthographicSize
        float newOrthographicSize = m_camera.orthographicSize + zoomDelta;

        // Clamp the new orthographic size to be within the max and min zoom limits
        newOrthographicSize = Mathf.Clamp(newOrthographicSize, m_maxZoomIn, m_maxZoomOut);

        // Set the camera's orthographic size
        m_camera.orthographicSize = newOrthographicSize;
    }

    public void MoveToPoint(Vector3 point)
    {
        StopAllCoroutines();
        StartCoroutine(Move(point));
    }

    private IEnumerator Move(Vector3 point)
    {
        // While the distance between the character and the point is greater than a small number
        while (Vector3.Distance(transform.position, point) > 0.001f)
        {
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, point, m_speed * Time.deltaTime);

            // Yield until the next frame
            yield return null;
        }

        transform.position = point;
    }
}
