using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string _name;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _movementDistance = 1;

    [Header("Action Economy")]
    [SerializeField] protected bool m_usedMovement = false;
    [SerializeField] protected bool m_usedAction = false;
    [SerializeField] public bool HasEndedTurn = false;

    public virtual void Start()
    {
        _renderer.sprite = _sprite;
        name = _name;
    }

    public void MoveToPoint(Vector3 point)
    {
        if (m_usedMovement)
        {
            return;
        }

        if (!IsPositionValid(point))
        {
            return;
        }

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

        m_usedMovement = true;
        LocalPlayerActions.Instance.MovementComplete();
    }

    private bool IsPositionValid(Vector3 point)
    {
        //Offset to ensure that point checked is in center of tiles, stops collision issues.
        point = new Vector3(point.x, point.y + GameSettings.Instance.GridTileCollisionOffset, point.z);

        if (Physics2D.OverlapPoint(point, GameSettings.Instance.InteractableLayer))
        {
            //TODO Provide feedback to player as to why they can't move here
            return false;
        }

        if (Vector3.Distance(transform.position, point) < _movementDistance)
        {
            //TODO Provide feedback to player as to why they can't move here
            return true;
        }

        return false;
    }

    private IEnumerator Move(Vector3 point)
    {
        while (Vector3.Distance(transform.position, point) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, _speed * Time.deltaTime);

            yield return null;
        }

        transform.position = point;
    }

    public void EndTurn()
    {
        HasEndedTurn = true;
    }

    public void Reset()
    {
        m_usedMovement = false;
        m_usedAction = false;
        HasEndedTurn = false;
    }
}