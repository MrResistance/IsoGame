using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private Tile m_tile;

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
        if (m_tilemap.GetTile(cellPosition) != m_tile)
        {
            m_tilemap.ClearAllTiles();
            m_tilemap.SetTile(cellPosition, m_tile);
        }
    }

    public void ClearAllTiles()
    {
        m_tilemap.ClearAllTiles();
    }
}
