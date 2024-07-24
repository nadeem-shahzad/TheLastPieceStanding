using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayPanelView : UIView
{

    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Text m_CurrencyText;
    [SerializeField] private PieceCard m_CardPrefab;
    [SerializeField] private Transform m_CardPrefabParent;
    [SerializeField] private Button m_ShopButton;
    [SerializeField] private Button m_FreeCoinsButton;
    [SerializeField] private Button m_RemoveAds;
    [SerializeField] private Button m_CollectionButton;
    

    public PieceCard SelectedCard { get; set; } = null;

    public override void Initialize()
    {
        m_SettingsButton.onClick.AddListener(OnSettingsClick);
        m_ShopButton.onClick.AddListener(OnShopClick);
        m_FreeCoinsButton.onClick.AddListener(OnFreeCoinsClick);
        m_RemoveAds.onClick.AddListener(OnRemoveAdsClick);
        m_CollectionButton.onClick.AddListener(OnCollectionClick);
        UIEvents.m_gameplayScoreUpdate += ScoreUpdate;
        UIEvents.a_UpdateCoins += CurrencyUpdate;
        UIEvents.a_DeleteSelectedCard += DeleteSelectedCard;
    }

    private void OnCollectionClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.Show<ChessShopView>();
    }

    private void CurrencyUpdate(int currency)
    {
        m_CurrencyText.text = currency.ToString();
    }

    private void ScoreUpdate(int scoreValue)
    {
        m_ScoreText.text = scoreValue.ToString();
    }

    private void OnSettingsClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.ShowPopUp<SettingPanelView>();
    }

    private void OnShopClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.Show<CoinShopView>();
    }
    
    private void OnFreeCoinsClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.ShowPopUp<FreeCoinsView>();
    }
    private void OnRemoveAdsClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.ShowPopUp<RemoveAdsView>();
    }


    public void InitializePieceCards(List<Piece> pieces)
    {
        foreach (var piece in pieces)
        {
            var card = Instantiate(m_CardPrefab, m_CardPrefabParent);
            card.Init(piece.PieceImage, piece.PieceName, this);
        }
    }

    private void DeleteSelectedCard()
    {
        if (!SelectedCard)
            return;
        
        Destroy(SelectedCard.gameObject);
    }
    
    
    

    private void OnDestroy()
    {
        UIEvents.m_gameplayScoreUpdate -= ScoreUpdate;
        UIEvents.m_gameplayCurrencyUpdate -= CurrencyUpdate;
        UIEvents.a_DeleteSelectedCard -= DeleteSelectedCard;
        UIEvents.a_UpdateCoins -= CurrencyUpdate;
    }
}
