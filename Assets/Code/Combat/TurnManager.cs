using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public enum TurnState { LocalPlayer, RemotePlayer };
    public TurnState State;

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
        StartPlayerTurn();
    }

    public void StartPlayerTurn()
    {
        State = TurnState.LocalPlayer;

        for (int i = 0; i < m_localPlayerCharacters.Count; i++)
        {
            m_localPlayerCharacters[i].ResetActionEconomy();
        }

        CurrentCharacter = m_localPlayerCharacters[0];
    }

    public void StartEnemyTurn()
    {
        State = TurnState.RemotePlayer;

        for (int i = 0; i < m_remotePlayerCharacters.Count; i++)
        {
            m_remotePlayerCharacters[i].ResetActionEconomy();
        }

        CurrentCharacter = m_remotePlayerCharacters[0];
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
