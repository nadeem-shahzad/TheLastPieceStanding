﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayPanelView : UIView
{

    [SerializeField] private Button m_SettingsButton;
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Text m_CurrencyText;
    [SerializeField] private TMP_Text m_LevelText;
    [SerializeField] private PieceCard m_CardPrefab;
    [SerializeField] private Transform m_CardPrefabParent;
    [SerializeField] private Button m_ShopButton;
    [SerializeField] private Button m_FreeCoinsButton;
    [SerializeField] private Button m_RemoveAds;
    [SerializeField] private Button m_CollectionButton;
    public PieceCard SelectedCard { get; set; } = null;
    private int m_Level = 0;
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

        // UIEvents.a_UpdateLevelText = null;
        // UIEvents.a_UpdateLevelText += UpdateLevelText;
        m_Level = PlayerPrefs.GetInt(Constants.LevelsKey, 0) + 1;
        UpdateLevelText(m_Level);
        UIEvents.a_UpdateFreeCoinsButton += UpdateFreeCoinsButtonActiveState;
        UIEvents.a_UpdateFreeCoinsButton?.Invoke(CanClaim(DateTime.Now));

        Invoke(nameof(CheckRateUsPanel),2f);
            
    }


    private void CheckRateUsPanel()
    {
        if (DataManager.Instance.IsRated == false && Constants.IsRateUsPanelShowed == false && m_Level > 5)
        {
            UIViewManager.ShowPopUp<RateUsPopup>();
            Constants.IsRateUsPanelShowed = true;
        }
    }

    private void UpdateFreeCoinsButtonActiveState(bool status)
    {
        m_FreeCoinsButton.gameObject.SetActive(status);
    }
    
    private void OnCollectionClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.Show<ChessShopView>();
        GameManager.Instance.IsGameEnded = true;
    }

    private void CurrencyUpdate(int currency)
    {
        m_CurrencyText.text = currency.ToString();
    }

    private void UpdateLevelText(int level)
    {
        m_LevelText.text = $"Level : {level}";
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
        GameManager.Instance.IsGameEnded = true;

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
    
    private bool CanClaim(DateTime currentTime)
    {
        if (PlayerPrefs.HasKey(Constants.LastFreeCoinsClaimed))
        {
            string lastClaimTimeString = PlayerPrefs.GetString(Constants.LastFreeCoinsClaimed);
            DateTime lastClaimTime = DateTime.Parse(lastClaimTimeString);

            return (currentTime - lastClaimTime).TotalHours >= 24;
        }
        return true;
    }
    

    private void OnDestroy()
    {
        UIEvents.m_gameplayScoreUpdate -= ScoreUpdate;
        UIEvents.m_gameplayCurrencyUpdate -= CurrencyUpdate;
        UIEvents.a_DeleteSelectedCard -= DeleteSelectedCard;
        UIEvents.a_UpdateCoins -= CurrencyUpdate;
    }
}
