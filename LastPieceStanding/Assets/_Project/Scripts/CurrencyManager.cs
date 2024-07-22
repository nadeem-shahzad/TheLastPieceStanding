using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    private int m_Coins;

    private int Coins
    {
        get => PlayerPrefs.GetInt(Constants.CoinsKey,50);
        set
        {
            PlayerPrefs.SetInt(Constants.CoinsKey, value);
            PlayerPrefs.Save();
            UIEvents.a_UpdateCoins?.Invoke(m_Coins);
        }
    }


    public void AddCoins(int amount)
    {
        Coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
