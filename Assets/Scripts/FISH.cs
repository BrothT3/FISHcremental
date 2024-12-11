using Unity.VisualScripting;
using UnityEngine;

public class FISH : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float Hunger = 0;
    public GameObject coinPrefab;
    public float coinDropInterval = 5f;
    public float coinValue = 10f;

    private void Start()
    {
        // Start dropping coins at regular intervals
        InvokeRepeating(nameof(DropCoin), coinDropInterval, coinDropInterval);
    }
    private void DropCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        FishCoin coinScript = coin.GetComponent<FishCoin>();
        if (coinScript != null)
        {
            coinScript.SetupCoin(coinValue);
        }
    }

}

