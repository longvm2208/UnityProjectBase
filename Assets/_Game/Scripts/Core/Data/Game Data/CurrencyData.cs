using System;
using UnityEngine;

[System.Serializable]
public class CurrencyData
{
    public event Action OnCoinAmountChanged;

    public int coinAmount;

    public CurrencyData()
    {
        coinAmount = 0;
    }

    public void ChangeCoinAmount(int amount, string place = null)
    {
        if (coinAmount + amount < 0)
        {
            Debug.LogError("Coin amount need to be greater than or equal to 0");
            return;
        }
        coinAmount += amount;
        OnCoinAmountChanged?.Invoke();
    }
}
