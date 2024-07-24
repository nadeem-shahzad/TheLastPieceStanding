using System.Collections;
using System.Collections.Generic;
using _Project.UI_Architecture.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class FreeCoinsView : PopUp
{
    [SerializeField] private Button m_CrossButton;
    [SerializeField] private Button m_DoubleCoins;
    [SerializeField] private Button m_CollectCoins;
    [SerializeField] private int m_Coins = 25;
    public override void Initialize()
    {
        m_CrossButton.onClick.AddListener(()=>
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
            UIViewManager.HidePopUp();
        });
        
        m_CollectCoins.onClick.AddListener(CollectCoins);
        m_DoubleCoins.onClick.AddListener(DoubleCoins);
    }


    private void CollectCoins()
    { 
        CurrencyManager.Instance.AddCoins(m_Coins);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.HidePopUp();
    }

    private void DoubleCoins()
    {
        CurrencyManager.Instance.AddCoins(m_Coins * 2);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.HidePopUp();
    }
    
}
