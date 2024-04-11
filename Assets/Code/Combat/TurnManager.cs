using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public enum TurnState { LocalPlayer, RemotePlayer };
    private TurnState m_state;
    public TurnState State
    {
        get { return m_state; }
        set
        {
            m_state = value;
            if (m_state == TurnState.LocalPlayer)
            {
                OnTurnStateChanged?.Invoke("Your Turn");
            }
            else
            {
                OnTurnStateChanged?.Invoke("Enemy Turn");
            }
        }
    }

    public event Action<string> OnTurnStateChanged;
    [SerializeField] private CameraController m_cameraController;

    [SerializeField] private Transform m_localPlayerTeam;
    [SerializeField] private Transform m_remotePlayerTeam;

    [SerializeField] private List<LocalPlayerCharacter> m_localPlayerCharacters;
    [SerializeField] private List<RemotePlayerCharacter> m_remotePlayerCharacters;

    public Character CurrentCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GetLocalPlayerCharacters();
        GetRemotePlayerCharacters();
        StartLocalPlayerTurn(m_localPlayerCharacters[0]);
    }

    public void StartLocalPlayerTurn(Character character)
    {
        State = TurnState.LocalPlayer;

        //for (int i = 0; i < m_localPlayerCharacters.Count; i++)
        //{
        //    m_localPlayerCharacters[i].Reset();
        //}

        CurrentCharacter = character;
        m_cameraController.MoveToPoint(CurrentCharacter.transform.position);
    }

    public void StartRemotePlayerTurn(Character character)
    {
        State = TurnState.RemotePlayer;

        //for (int i = 0; i < m_remotePlayerCharacters.Count; i++)
        //{
        //    m_remotePlayerCharacters[i].Reset();
        //}

        CurrentCharacter = character;
        m_cameraController.MoveToPoint(CurrentCharacter.transform.position);
    }

    public void StartNextTurn()
    {
        for (int i = 0; i < m_localPlayerCharacters.Count; i++)
        {
            if (!m_localPlayerCharacters[i].HasEndedTurn)
            {
                StartLocalPlayerTurn(m_localPlayerCharacters[i]);
                return;
            }
        }

        for (int j = 0; j < m_remotePlayerCharacters.Count; j++)
        {
            if (!m_remotePlayerCharacters[j].HasEndedTurn)
            {
                StartRemotePlayerTurn(m_remotePlayerCharacters[j]);
                return;
            }
        }
    }

    private void GetLocalPlayerCharacters()
    {
        for (int i = 0; i < m_localPlayerTeam.childCount; i++)
        {
            m_localPlayerTeam.GetChild(i).TryGetComponent(out LocalPlayerCharacter localPlayerCharacter);
            m_localPlayerCharacters.Add(localPlayerCharacter);
        }
    }

    private void GetRemotePlayerCharacters()
    {
        for (int i = 0; i < m_remotePlayerTeam.childCount; i++)
        {
            m_remotePlayerTeam.GetChild(i).TryGetComponent(out RemotePlayerCharacter remotePlayerCharacter);
            m_remotePlayerCharacters.Add(remotePlayerCharacter);
        }
    }
}
