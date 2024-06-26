using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] private Vector3 m_worldPosition;
    [SerializeField] private Vector3Int m_cellPosition;

    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private Tile m_HighlightWhite;
    [SerializeField] private Tile m_HighlightRed;
    [SerializeField] private Tile m_HighlightBlue;
    [SerializeField] private Character m_highlightedCharacter;
    [SerializeField] private GreyscaleControl m_highlightedCharacterGreyscale;
    private void OnEnable()
    {
        PlayerInputs.Instance.OnCursorMoved += UpdateTiles;
        m_tilemap.SetTile(m_tilemap.WorldToCell(RoundManager.Instance.CurrentCharacter.transform.position), m_HighlightWhite);
    }
    private void OnDisable()
    {
        if (m_highlightedCharacter != null)
        {
            m_highlightedCharacter.DisableOverheadSprite();
        }
        
        m_tilemap.ClearAllTiles();
        PlayerInputs.Instance.OnCursorMoved -= UpdateTiles;
    }
    private void UpdateTiles()
    {
        GetCurrentCellUnderCursor();
        HighlightTileAt(m_cellPosition);
    }

    private void GetCurrentCellUnderCursor()
    {
        m_worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_cellPosition = m_tilemap.WorldToCell(m_worldPosition);
    }

    private void HighlightTileAt(Vector3Int cellPosition)
    {
        if (m_tilemap.GetTile(cellPosition) == null)
        {
            m_tilemap.ClearAllTiles();
            m_tilemap.SetTile(m_tilemap.WorldToCell(RoundManager.Instance.CurrentCharacter.transform.position), m_HighlightWhite);
            ColorCheck(cellPosition);
        }
    }

    private void ColorCheck(Vector3Int cellPosition)
    {
        //To ensure that the overhead sprites are disabled when the player moves away from the character
        if (m_highlightedCharacter != null)
        {
            m_highlightedCharacter.DisableOverheadSprite();
            m_highlightedCharacter.TryGetComponent(out SpriteRenderer renderer);
            renderer.sortingOrder = m_highlightedCharacter.BaseSortOrder;
        }

        Vector3 point = m_tilemap.CellToWorld(cellPosition);

        point = new Vector3(point.x, point.y + GameSettings.Instance.GridTileCollisionOffsetY, point.z);

        Collider2D hitCollider = Physics2D.OverlapPoint(point, GameSettings.Instance.InteractableLayer);

        if (hitCollider != null)
        {
            if (hitCollider.TryGetComponent(out Character character))
            {
                m_highlightedCharacter = character;
            }
        }
        else
        {
            m_highlightedCharacter = null;
        }

        switch (LocalPlayerActions.Instance.CurrentSelection)
        {
            case LocalPlayerActions.ActionSelection.nothing:
                break;
            case LocalPlayerActions.ActionSelection.movement:
                if (Vector3.Distance(cellPosition, m_tilemap.WorldToCell(RoundManager.Instance.CurrentCharacter.transform.position))
                    <= RoundManager.Instance.CurrentCharacter.MovementRemaining
                    && !Physics2D.OverlapPoint(point, GameSettings.Instance.InteractableLayer))
                {
                    m_tilemap.SetTile(cellPosition, m_HighlightBlue);
                }
                else
                {
                    m_tilemap.SetTile(cellPosition, m_HighlightRed);
                }
                break;
            case LocalPlayerActions.ActionSelection.attack:
                if (Vector3.Distance(cellPosition, m_tilemap.WorldToCell(RoundManager.Instance.CurrentCharacter.transform.position))
                    <= RoundManager.Instance.CurrentCharacter.AttackRange
                    && m_highlightedCharacter != null)
                {
                    if (m_highlightedCharacter.TryGetComponent(out RemotePlayerCharacter _))
                    {
                        m_highlightedCharacter.TryGetComponent(out SpriteRenderer renderer);
                        renderer.sortingOrder = m_highlightedCharacter.ActiveSortOrder;
                        m_highlightedCharacter.DisplayTargetedSprite();

                        m_highlightedCharacter.TryGetComponent(out GreyscaleControl greyscaleControl);
                        m_highlightedCharacterGreyscale = greyscaleControl;
                        m_highlightedCharacterGreyscale.ApplyGreyscale(0);

                        m_tilemap.SetTile(cellPosition, m_HighlightBlue);
                    }
                }
                else
                {
                    if (m_highlightedCharacter != null)
                    {
                        m_highlightedCharacter.DisableOverheadSprite();
                    }

                    if (m_highlightedCharacterGreyscale != null)
                    {
                        m_highlightedCharacterGreyscale.ApplyGreyscale(1);
                    }

                    m_tilemap.SetTile(cellPosition, m_HighlightRed);
                }
                break;
            default:
                break;
        }
    }

    public void ClearAllTiles()
    {
        m_tilemap.ClearAllTiles();
    }
}
