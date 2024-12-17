using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    private float Coins;
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    [SerializeField]
    private FishObject[] fISHes;
    // Event to notify when Coins change
    public event Action<float> OnCoinsChanged;

    public void Start()
    {
        FishCoin.OnCoinClicked += HandleCoinClicked;
    }
    public void AddCoins(float amount)
    {
        Coins += amount;
        Debug.Log($"Coins increased by {amount}. Total coins: {Coins}");

        // Notify listeners about the updated coin count
        OnCoinsChanged?.Invoke(Coins);
    }
    private void HandleCoinClicked(float coinValue)
    {
        AddCoins(coinValue);
    }

    public bool CheckPurchase(string s, int i)
    {
        if (!resources.ContainsKey(s))
        {
            Debug.Log($"{s} is missing");
            return false;
        }

        if (resources[s] >= i)
            return true;
        else
            return false;
    }  
    public void PurchaseFish(string s, int i, int fishIndex)
    {
        if (!CheckPurchase(s, i))
            return;

        resources[s] -= i;
        GameObject go = Instantiate(GameManager.Instance.FishPrefab);
        go.GetComponent<FISH>().SetupFish(fISHes[fishIndex]);
        Instantiate(go);
    }
}


