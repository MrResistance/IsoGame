using TMPro;
using UnityEngine;

public class ScreenspaceUIManager : MonoBehaviour
{
    public static ScreenspaceUIManager Instance;

    [SerializeField] private TextMeshProUGUI m_interactText;

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

    public void UpdateInteractText(string text)
    {
        m_interactText.text = text;
    }

    public void ClearInteractText()
    {
        m_interactText.text = "";
    }
}
