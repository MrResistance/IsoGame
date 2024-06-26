using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public LayerMask InteractableLayer;
    public float GridTileCollisionOffsetY = 0.25f;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
