using System;
using TMPro;
using UnityEngine;

public class ScreenspaceUIManager : MonoBehaviour
{
    public static ScreenspaceUIManager Instance;

    [SerializeField] private TextMeshProUGUI m_turnStateText;
    [SerializeField] private TextMeshProUGUI m_characterNameText;
    [SerializeField] private TextMeshProUGUI m_roundCountText;

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
        RoundManager.Instance.OnTurnStateChanged += UpdateTurnStateText;
        RoundManager.Instance.OnCurrentCharacterChanged += UpdateCharacterNameText;
        RoundManager.Instance.OnRoundCountChanged += UpdateRoundCountText;
    }

    private void OnDisable()
    {
        RoundManager.Instance.OnTurnStateChanged -= UpdateTurnStateText;
        RoundManager.Instance.OnCurrentCharacterChanged -= UpdateCharacterNameText;
        RoundManager.Instance.OnRoundCountChanged -= UpdateRoundCountText;
    }

    private void UpdateRoundCountText(int value)
    {
        m_roundCountText.text = value.ToString();
    }

    public void UpdateTurnStateText(string text)
    {
        m_turnStateText.text = text;
    }

    public void ClearTurnStateText()
    {
        m_turnStateText.text = "";
    }

    public void UpdateCharacterNameText(string text)
    {
        m_characterNameText.text = text;
    }

    public void ClearCharacterNameText()
    {
        m_characterNameText.text = "";
    }
}
