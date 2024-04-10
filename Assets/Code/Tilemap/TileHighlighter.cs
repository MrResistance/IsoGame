using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHighlighter : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private Tile m_tile;

    private void FixedUpdate()
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
}
