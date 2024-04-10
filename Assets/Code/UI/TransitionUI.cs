using UnityEngine;
using System.Collections;
public class TransitionUI : MonoBehaviour
{
    [SerializeField] private float m_speed = 5;
    [SerializeField] private Vector3 m_onScreenPosition;
    [SerializeField] private Vector3 m_offScreenPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("Offscreen")]
    public void TransitionOffScreen()
    {
        MoveToPoint(m_offScreenPosition);
    }

    [ContextMenu("Onscreen")]
    public void TransitionOnScreen()
    {
        MoveToPoint(m_onScreenPosition);
    }

    public void MoveToPoint(Vector3 point)
    {
        StopAllCoroutines();
        StartCoroutine(Move(point));
    }

    private IEnumerator Move(Vector3 point)
    {
        // While the distance between the character and the point is greater than a small number
        while (Vector3.Distance(transform.localPosition, point) > 0.001f)
        {
            // Move our position a step closer to the target.
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, point, m_speed * Time.deltaTime);

            // Yield until the next frame
            yield return null;
        }

        transform.localPosition = point;
    }
}
