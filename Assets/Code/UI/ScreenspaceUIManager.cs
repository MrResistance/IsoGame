using TMPro;
using UnityEngine;

public class ScreenspaceUIManager : MonoBehaviour
{
    public static ScreenspaceUIManager Instance;

    [SerializeField] private TextMeshProUGUI m_turnStateText;
    [SerializeField] private TextMeshProUGUI m_characterNameText;

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
        TurnManager.Instance.OnTurnStateChanged += UpdateTurnStateText;
        TurnManager.Instance.OnCurrentCharacterChanged += UpdateCharacterNameText;
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnTurnStateChanged -= UpdateTurnStateText;
        TurnManager.Instance.OnCurrentCharacterChanged -= UpdateCharacterNameText;
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
