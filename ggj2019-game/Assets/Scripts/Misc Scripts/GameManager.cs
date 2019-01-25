using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public float totalCoins;
    [HideInInspector] public float totalSkulls;

    #region Singleton Setup

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // to keep gamemanager running on start
        // DontDestroyOnLoad(gameObject);
    }

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
