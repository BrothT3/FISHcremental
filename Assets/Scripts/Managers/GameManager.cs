using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject CoinObj;
    public GameObject FishPrefab;
    public InputManager IP;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
            Instance = this;

        Application.targetFrameRate = 60;
    }
    
}
