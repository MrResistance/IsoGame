using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private Tile m_HighlightWhite;
    [SerializeField] private Tile m_HighlightRed;
    [SerializeField] private Tile m_HighlightBlue;

    private void OnEnable()
    {
        PlayerInputs.Instance.OnCursorMoved += UpdateTiles;
    }
    private void OnDisable()
    {
        PlayerInputs.Instance.OnCursorMoved -= UpdateTiles;
    }
    private void UpdateTiles()
    {
        Vector3Int highlightCellPosition = GetCurrentCellUnderCursor();
        HighlightTileAt(highlightCellPosition);
    }

    private Vector3Int GetCurrentCellUnderCursor()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = m_tilemap.WorldToCell(worldPosition);
        return cellPosition;
    }

    private void HighlightTileAt(Vector3Int cellPosition)
    {
        if (m_tilemap.GetTile(cellPosition) == null)
        {
            m_tilemap.ClearAllTiles();
            ColorCheck(cellPosition);
        }
    }

    private void ColorCheck(Vector3Int cellPosition)
    {
        switch (LocalPlayerActions.Instance.CurrentSelection)
        {
            case LocalPlayerActions.ActionSelection.nothing:
                break;
            case LocalPlayerActions.ActionSelection.movement:
                if (Vector3.Distance(cellPosition, RoundManager.Instance.CurrentCharacter.transform.position) <= RoundManager.Instance.CurrentCharacter.MovementDistance)
                {
                    m_tilemap.SetTile(cellPosition, m_HighlightBlue);
                }
                else
                {
                    m_tilemap.SetTile(cellPosition, m_HighlightRed);
                }
                break;
            case LocalPlayerActions.ActionSelection.attack:
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
