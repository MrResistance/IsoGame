using UnityEngine;

public class LocalPlayerActions : MonoBehaviour
{
    public static LocalPlayerActions Instance;

    [SerializeField] private TileHighlighter m_tileHighlighter;
    [SerializeField] private TileInteractor m_tileInteractor;

    [SerializeField] private TransitionUI m_move;
    [SerializeField] private TransitionUI m_attack;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Move()
    {
        m_tileHighlighter.enabled = true;
        m_tileInteractor.enabled = true;
    }

    public void MovementComplete()
    {
        m_tileHighlighter.enabled = false;
        m_tileInteractor.enabled = false;
        m_move.TransitionOffScreen();
    }
    public void Attack()
    {
        
    }
}
