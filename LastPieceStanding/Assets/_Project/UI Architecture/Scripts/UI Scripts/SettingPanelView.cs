using System.Collections;
using System.Collections.Generic;
using _Project.UI_Architecture.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelView : PopUp
{
    [SerializeField] private Button m_BackButton;
    [SerializeField] private Toggle m_SoundToggle;
    [SerializeField] private Toggle m_HepticsToggle;
    [SerializeField] private Button m_ShareUsButton;
    [SerializeField] private Button m_RateUsButton;
    [SerializeField] private Button m_PrivacyPolicy;

    [SerializeField] private Texture2D m_Icon;
    //

    public override void Initialize()
    {
        UIEvents.m_SoundToggleUpdate = null;
        UIEvents.m_SoundToggleUpdate += SoundToggleUpdate;
        UIEvents.m_HepticsToggleUpdate = null;
        UIEvents.m_HepticsToggleUpdate += HapticsToggleUpdate;
        m_BackButton.onClick.AddListener(OnBackClick);
        m_PrivacyPolicy.onClick.AddListener(PrivacyPolicy);
        m_ShareUsButton.onClick.AddListener(ShareUs);
        m_RateUsButton.onClick.AddListener(RateUs);
    }
   
    private void SoundToggleUpdate(bool sound)
    {
        m_SoundToggle.isOn = sound;
        m_SoundToggle.onValueChanged.RemoveAllListeners();
        m_SoundToggle.onValueChanged.AddListener(OnSoundToggleValueChange);
    
    }
    private void HapticsToggleUpdate(bool haptics)
    {
        m_HepticsToggle.isOn = haptics;
        m_HepticsToggle.onValueChanged.RemoveAllListeners();
        m_HepticsToggle.onValueChanged.AddListener(OnHapticsToggleValueChange);
    }
    
    
    private void OnBackClick()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        UIViewManager.HidePopUp();
    }
    
    private void OnSoundToggleValueChange(bool value)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        DataManager.Instance.IsSoundOn = value;
    }
    
    private void OnHapticsToggleValueChange(bool value)
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        DataManager.Instance.IsHapticsOn = value;
    }

    private void RateUs()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        Application.OpenURL(Constants.ApplicationLink);
    }

    private void ShareUs()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        new NativeShare().AddFile( m_Icon,"Checkmated King" )
            .SetSubject( Application.productName ).SetText( "Download Checkmated King now and join me in defending the realm. Let's see if you have what it takes to save the kingdom! \ud83d\udee1\ufe0f\u2694\ufe0f" ).SetUrl(Constants.ApplicationLink)
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }

    private void PrivacyPolicy()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        Application.OpenURL(Constants.PrivacyLink);
    }
}
