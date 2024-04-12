using UnityEngine;

public class LocalPlayerActions : MonoBehaviour
{
    public static LocalPlayerActions Instance;

    [SerializeField] private TileHighlighter m_tileHighlighter;
    [SerializeField] private TileInteractor m_tileInteractor;

    [SerializeField] private TransitionUI m_move;
    [SerializeField] private TransitionUI m_attack;
    [SerializeField] private TransitionUI m_endTurn;

    public enum ActionSelection { nothing, movement, attack }

    public ActionSelection CurrentSelection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void ActionsOnScreen()
    {
        m_attack.TransitionOnScreen();
        m_move.TransitionOnScreen();
        m_endTurn.TransitionOnScreen();
    }

    public void ActionsOffScreen()
    {
        m_attack.TransitionOffScreen();
        m_move.TransitionOffScreen();
        m_endTurn.TransitionOffScreen();
    }

    public void Move()
    {
        CurrentSelection = ActionSelection.movement;
        m_tileHighlighter.enabled = true;
        m_tileInteractor.enabled = true;
    }

    public void MovementComplete()
    {
        CurrentSelection = ActionSelection.nothing;
        m_tileHighlighter.ClearAllTiles();
        m_tileHighlighter.enabled = false;
        m_tileInteractor.enabled = false;
        m_move.TransitionOffScreen();
    }

    public void Attack()
    {
        CurrentSelection = ActionSelection.attack;
        m_tileHighlighter.enabled = true;
        m_tileInteractor.enabled = true;
    }

    public void AttackComplete()
    {
        CurrentSelection = ActionSelection.nothing;
        m_tileHighlighter.ClearAllTiles();
        m_tileHighlighter.enabled = false;
        m_tileInteractor.enabled = false;
        m_attack.TransitionOffScreen();
    }

    public void EndTurn() 
    {
        RoundManager.Instance.CurrentCharacter.EndTurn();
        RoundManager.Instance.StartNextTurn();
        ActionsOnScreen();
    }
}