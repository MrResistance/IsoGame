using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerActions : MonoBehaviour
{
    public static LocalPlayerActions Instance;

    [SerializeField] private TileHighlighter m_tileHighlighter;
    [SerializeField] private TileInteractor m_tileInteractor;

    [SerializeField] private TransitionUI m_move;
    [SerializeField] private TransitionUI m_attack;
    [SerializeField] private TransitionUI m_endTurn;

    [SerializeField] private GreyscaleControl m_ground;
    [SerializeField] private List<GreyscaleControl> m_characters;
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

    private void Start()
    {
        if (PlayerInputs.Instance != null)
        {
            PlayerInputs.Instance.OnSecondaryPressed += ActionComplete;
        }
    }

    private void OnEnable()
    {
        if (PlayerInputs.Instance != null) 
        {
            PlayerInputs.Instance.OnSecondaryPressed += ActionComplete;
        }
    }

    private void OnDisable()
    {
        PlayerInputs.Instance.OnSecondaryPressed -= ActionComplete;
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

    public void EnableMovement()
    {
        CurrentSelection = ActionSelection.movement;
        m_tileHighlighter.enabled = true;
        m_tileInteractor.enabled = true;
        ChangeCharacterListGreyscale(1);
        ChangeCurrentCharacterGreyscale(0);
    }

    public void MovementComplete()
    {
        ActionComplete();
        m_move.TransitionOffScreen();
    }

    public void EnableAttacking()
    {
        CurrentSelection = ActionSelection.attack;
        m_tileHighlighter.enabled = true;
        m_tileInteractor.enabled = true;
        RoundManager.Instance.CurrentCharacter.DisplayAttackngSprite();
        ChangeCharacterListGreyscale(1);
        ChangeCurrentCharacterGreyscale(0);
    }
    
    public void AttackComplete()
    {
        ActionComplete();   
        m_attack.TransitionOffScreen();
    }

    /// <summary>
    /// Shares functionality with cancelling an action I.E. if a player changes their mind and wants to do something else
    /// </summary>
    public void ActionComplete()
    {
        RoundManager.Instance.CurrentCharacter.DisableOverheadSprite();
        CurrentSelection = ActionSelection.nothing;
        m_tileHighlighter.enabled = false;
        m_tileInteractor.enabled = false;
        ChangeCharacterListGreyscale(0);
    }

    public void EndTurn() 
    {
        m_tileHighlighter.enabled = false;
        m_tileInteractor.enabled = false;
        RoundManager.Instance.CurrentCharacter.EndTurn();
        RoundManager.Instance.StartNextTurn();
        ChangeCharacterListGreyscale(0);
        ActionsOnScreen();
    }

    private void ChangeCharacterListGreyscale(float amount)
    {
        for (int i = 0; i < m_characters.Count; i++)
        {
            m_characters[i].ApplyGreyscale(amount);
        }
    }

    private void ChangeCurrentCharacterGreyscale(float amount)
    {
        for (int i = 0; i < m_characters.Count; i++)
        {
            if (m_characters[i].gameObject == RoundManager.Instance.CurrentCharacter.gameObject)
            {
                m_characters[i].ApplyGreyscale(amount);
            }
        }
    }
}