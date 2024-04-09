using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public enum TurnState { Player, Enemy };
    public TurnState State;

    private void Start()
    {
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        State = TurnState.Player;
    }

    public void StartEnemyTurn()
    {
        State = TurnState.Enemy;
    }
}
