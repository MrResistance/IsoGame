using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInteractor : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;
    [SerializeField] private Character m_currentCharacter;
    private void Start()
    {
        PlayerInputs.Instance.OnPrimaryPressed += GetGridCoordinate;
    }

    private void GetGridCoordinate()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        Vector3Int cellPosition = m_tilemap.WorldToCell(worldPosition);

        Debug.Log($"Grid Coordinate: {cellPosition}");

        m_currentCharacter.MoveToPoint(m_tilemap.CellToWorld(cellPosition));
    }
}
