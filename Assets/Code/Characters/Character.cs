using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected float _speed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _renderer.sprite = _sprite;
    }

    public void MoveToPoint(Vector3 point)
    {
        if (transform.localPosition.x - point.x > 0)
        {
            _renderer.flipX = true;
        }
        else
        {
            _renderer.flipX = false;
        }
        StopAllCoroutines();
        StartCoroutine(Move(point));
    }

    private IEnumerator Move(Vector3 point)
    {
        // While the distance between the character and the point is greater than a small number
        while (Vector3.Distance(transform.position, point) > 0.001f)
        {
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);

            // Yield until the next frame
            yield return null;
        }

        transform.position = point;
    }
}
