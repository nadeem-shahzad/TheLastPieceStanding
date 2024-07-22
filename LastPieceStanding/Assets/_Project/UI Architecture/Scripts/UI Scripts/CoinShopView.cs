using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinShopView : UIView
{
    [SerializeField] private Button m_BackButton;
    [SerializeField] private Text m_CurrencyText;
    
    public override void Initialize()
    {
        m_BackButton.onClick.RemoveAllListeners();
        m_BackButton.onClick.AddListener(BackButtonClick);
        UIEvents.a_UpdateCoins += UpdateCoins;
    }

    public void BuyItem(eShopItem itemType)
    {
        switch (itemType)
        {
            case eShopItem.RemoveAds:
                RemoveAllAds();
                break;
            case eShopItem.FreeCoins:
                AddCoins(75);
                break;
            case eShopItem.Coins1000:
                AddCoins(1000);
                break;
            case eShopItem.Coins3050:
                AddCoins(3050);
                break;
            case eShopItem.Coins7000:
                AddCoins(7000);
                break;
            case eShopItem.Coins15000:
                AddCoins(15000);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
        }
    }

    private void RemoveAllAds()
    {
        
    }

    private void AddCoins(int coins)
    {
        CurrencyManager.Instance.AddCoins(coins);
    }

    private void UpdateCoins(int coins)
    {
        m_CurrencyText.text = coins.ToString();
    }

    private void BackButtonClick()
    {
        UIViewManager.ShowLast();
    }

    private void OnDestroy()
    {
        UIEvents.a_UpdateCoins -= UpdateCoins;
    }
}

public enum eShopItem
{
    RemoveAds, FreeCoins, Coins1000, Coins3050, Coins7000, Coins15000 
}