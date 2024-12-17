using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject CoinObj;
    public GameObject FishPrefab;
    public InputManager IP;
    public GameObject FishContainer;
    public GameObject CoinContainer;
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
