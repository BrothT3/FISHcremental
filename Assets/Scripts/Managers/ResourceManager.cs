using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    public Dictionary<string, int> resources = new Dictionary<string, int>();
    public TextMeshProUGUI CoinsText;
    // Event to notify when Coins change
    public event Action<int> OnCoinsChanged;

    public void Start()
    {
        FishCoin.OnCoinClicked += HandleCoinClicked;
        resources.Add("coins", 2000);
        OnCoinsChanged?.Invoke(10);
    }
    public void AddResource(string Resource, int amount)
    {
        resources[Resource] += amount;
        Debug.Log($"Coins increased by {amount}. Total coins: {resources["coins"]}");

        // Notify listeners about the updated coin count
        int newAmount = resources[Resource];
        OnCoinsChanged?.Invoke(newAmount);
    }
    public void RemoveResource(string Resource, int amount)
    {
        resources[Resource] -= amount;
        Debug.Log($"Coins decreased by {amount}. Total coins: {resources["coins"]}");

        // Notify listeners about the updated coin count
        int newAmount = resources[Resource];
        OnCoinsChanged?.Invoke(newAmount);
    }
    private void HandleCoinClicked(float coinValue)
    {
        AddResource("coins", (int)coinValue);
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
        {
            Debug.Log($"can't afford cost of {i} with only {resources[s]} {s}");
            return false;
        }
            
    }

    public void PurchaseFish(string s, int i, FishObject FO)
    {
        if (!CheckPurchase(s, i))
            return;

        resources[s] -= i;
        GameObject go = Instantiate(GameManager.Instance.FishPrefab, GameManager.Instance.FishContainer.transform);
        go.GetComponent<FISH>().SetupFish(FO);
        int newAmount = resources[s];
        OnCoinsChanged?.Invoke(newAmount);
    }
}


