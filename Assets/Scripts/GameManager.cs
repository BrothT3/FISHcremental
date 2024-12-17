using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;

    public static GameManager Instance;
    public GameObject CoinObj;
    public InputManager IP;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
            instance = this;

        Application.targetFrameRate = 60;
        IP = new InputManager();
    }
    
}
