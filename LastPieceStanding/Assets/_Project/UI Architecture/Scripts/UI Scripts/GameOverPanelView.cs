using System;
using System.Collections;
using System.Collections.Generic;
using SupersonicWisdomSDK;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameOverPanelView : UIView
{
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Button m_WatchAdButton;
    [SerializeField] private Transform m_Emojies;
    [SerializeField] private TMP_Text m_CoinsText;
    
    [Header("Animation Related Things")] 
    [SerializeField] private GameObject m_UpperBar;
    [SerializeField] private GameObject[] m_BottomObjects;
    public override void Initialize()
    {
        m_RestartButton.onClick.AddListener(OnRestart);
        m_WatchAdButton.onClick.AddListener(OnDoubleClick);
        UIEvents.a_OnGameLose = null;
        UIEvents.a_OnGameLose += OnLoose;
    }

    private void OnLoose()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.LevelLose);
        m_CoinsText.text = $"Coins {Constants.LevelWinPrice / 2}";
        ShowEmoji();
        UIViewManager.Show(this,true);
        SupersonicWisdom.Api.NotifyLevelFailed(ESwLevelType.Regular,(long)(LevelManager.Instance.Level + 1),null);
        // PanelAnimations();
    }

    private void OnEnable()
    {
        PanelAnimations();
    }

    private void ShowEmoji()
    {
        var index = Random.Range(0, m_Emojies.childCount);
        m_Emojies.GetChild(index).gameObject.SetActive(true);
    }

    private void OnRestart()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        CurrencyManager.Instance.AddCoins(Constants.LevelWinPrice/2);
        
        if (LevelManager.Instance.Level >= 5)
            GoogleAdmobController.s_Instance.ShowAdInterstitial();
        
        SceneManager.LoadScene("Gameplay");
    }

    private void OnDoubleClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        GoogleAdmobController.s_Instance.ShowAdRewardedAd(OnVideoAddCompleted);
    }

    private void OnVideoAddCompleted()
    {
        CurrencyManager.Instance.AddCoins(Constants.LevelWinPrice);
        SceneManager.LoadScene("Gameplay");
    }
    
    private void PanelAnimations()
    {
        var uperHashtable = iTween.Hash("y", 1500, "time", 0.5f, "easetype", iTween.EaseType.linear,"islocal",true);
        iTween.MoveFrom(m_UpperBar,uperHashtable);
        
        var emojiHashtable = iTween.Hash("scale", Vector3.zero, "time", 0.5f, "delay", 0.5f, "easetype", iTween.EaseType.easeInBounce,"islocal",true);
        iTween.ScaleFrom(m_Emojies.gameObject,emojiHashtable);

        float delay = 0.25f;
        foreach (var bottomObject in m_BottomObjects)
        {
            var bottomHashtable = iTween.Hash("y", -1500, "time", 0.5f, "delay", delay, "easetype", iTween.EaseType.linear,"islocal",true);
            iTween.MoveFrom(bottomObject,bottomHashtable);
            delay += 0.25f;
        }
    }
    private void OnDestroy()
    {
        UIEvents.a_OnGameLose -= OnLoose;
    }
}
