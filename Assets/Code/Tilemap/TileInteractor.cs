using System.Drawing;
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

        switch (LocalPlayerActions.Instance.CurrentSelection)
        {
            case LocalPlayerActions.ActionSelection.nothing:
                break;
            case LocalPlayerActions.ActionSelection.movement:
                RoundManager.Instance.CurrentCharacter.TryMoveToPoint(m_tilemap.CellToWorld(cellPosition));
                break;
            case LocalPlayerActions.ActionSelection.attack:
                RoundManager.Instance.CurrentCharacter.TryAttack(worldPosition);
                break;
            default:
                break;
        }
    }


    /// <summary>
    ///  Dale
    /// </summary>
    private void OnDisable()
    {
        PlayerInputs.Instance.OnPrimaryPressed -= GetGridCoordinate;
    }
}
