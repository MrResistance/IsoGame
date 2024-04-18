using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected string _name;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected float _speed;

    [Header("Sprites")]
    public int BaseSortOrder = 0;
    public int ActiveSortOrder = 1;
    [SerializeField] private SpriteRenderer m_overheadRenderer;
    [SerializeField] private Sprite m_attackingSprite;
    [SerializeField] private Sprite m_targetedSprite;

    [Header("Action Economy")]
    [SerializeField] public int MovementRemaining = 0;
    [SerializeField] protected bool m_usedAction = false;
    [SerializeField] public bool HasEndedTurn = false;

    //TODO Read stats from scriptable object class "CharacterData/CharacterClass"
    [Header("Stats")]
    public int Damage = 1;
    public int AttackRange = 1;
    public int MovementDistance = 1;
    public virtual void Start()
    {
        _renderer.sprite = _sprite;
        _renderer.sortingOrder = BaseSortOrder;
        m_overheadRenderer.sortingOrder = BaseSortOrder;
        m_overheadRenderer.enabled = false;

        name = _name;
        MovementRemaining = MovementDistance;
    }

    public void StartTurn()
    {
        _renderer.sortingOrder = ActiveSortOrder;
        m_overheadRenderer.sortingOrder = ActiveSortOrder;
    }

    public void TryMoveToPoint(Vector3 point)
    {
        if (MovementRemaining == 0)
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

        MovementRemaining -= Mathf.RoundToInt(Vector3.Distance(transform.position, point) * 2);

        StopAllCoroutines();
        StartCoroutine(Move(point));
        
        if (MovementRemaining <= 0)
        {
            MovementRemaining = 0; 
            LocalPlayerActions.Instance.MovementComplete();
        }
    }

    private bool IsPositionValid(Vector3 point)
    {
        //Offset to ensure that point checked is in center of tiles, stops collision issues.
        point = new Vector3(point.x, point.y + GameSettings.Instance.GridTileCollisionOffsetY, point.z);

        if (Physics2D.OverlapPoint(point, GameSettings.Instance.InteractableLayer))
        {
            //TODO Provide feedback to player as to why they can't move here
            return false;
        }

        if (Vector3.Distance(transform.position, point) < MovementDistance)
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

    public void TryAttack(Vector3 position)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(position, GameSettings.Instance.InteractableLayer);

        if (hitCollider != null)
        {
            hitCollider.TryGetComponent(out Damageable damageable);
            if (damageable != null && Vector3.Distance(transform.position, damageable.transform.position) <= AttackRange)
            {
                ScreenShake.Instance.TriggerShake(0.1f, 0.01f, 0.01f);
                damageable.LoseHitPoints(RoundManager.Instance.CurrentCharacter.Damage);
                m_usedAction = true;
                LocalPlayerActions.Instance.AttackComplete();
            }
        }
    }

    public void EndTurn()
    {
        HasEndedTurn = true;
        _renderer.sortingOrder = BaseSortOrder;
        m_overheadRenderer.sortingOrder = BaseSortOrder;
    }

    public void Reset()
    {
        MovementRemaining = MovementDistance;
        m_usedAction = false;
        HasEndedTurn = false;
    }

    public void DisplayOverheadSprite(Sprite sprite)
    {
        m_overheadRenderer.enabled = true;
        m_overheadRenderer.sprite = sprite;
    }

    public void DisplayTargetedSprite()
    {
        DisplayOverheadSprite(m_targetedSprite);
    }

    public void DisplayAttackngSprite()
    {
        DisplayOverheadSprite(m_attackingSprite);
    }

    public void DisableOverheadSprite()
    {
        m_overheadRenderer.enabled = false;
    }
}