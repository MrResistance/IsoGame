using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteractor : MonoBehaviour
{
    [SerializeField] private Tilemap m_tilemap;
    private void OnEnable()
    {
        PlayerInputs.Instance.OnPrimaryPressed += GetGridCoordinate;
    }

    private void GetGridCoordinate()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        Vector3Int cellPosition = m_tilemap.WorldToCell(worldPosition);

        TurnManager.Instance.CurrentCharacter.MoveToPoint(m_tilemap.CellToWorld(cellPosition));
    }

    private void OnDisable()
    {
        PlayerInputs.Instance.OnPrimaryPressed -= GetGridCoordinate;
    }
}
