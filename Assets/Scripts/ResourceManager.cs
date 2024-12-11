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
}


