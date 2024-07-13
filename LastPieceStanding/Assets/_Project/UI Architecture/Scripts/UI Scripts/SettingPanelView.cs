using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelView : UIView
{
    [SerializeField] private Button m_BackButton;
    [SerializeField] private Toggle m_MusicToggle;
    [SerializeField] private Toggle m_SoundToggle;
    [SerializeField] private Toggle m_HepticsToggle;


    public override void Initialize()
    {
        UIEvents.m_MusicToggleUpdate += MusicToggleUpdate;
        UIEvents.m_SoundToggleUpdate += SoundToggleUpdate;
        UIEvents.m_HepticsToggleUpdate += HepticsToggleUpdate;
        
        m_BackButton.onClick.AddListener(OnBackClick);
    }

    private void MusicToggleUpdate(bool music)
    {
        m_MusicToggle.isOn = music;
        m_MusicToggle.onValueChanged.RemoveAllListeners();
        m_MusicToggle.onValueChanged.AddListener(OnMusicToggleValueChange);

    }
    private void SoundToggleUpdate(bool music)
    {
        m_SoundToggle.isOn = music;
        m_SoundToggle.onValueChanged.RemoveAllListeners();
        m_SoundToggle.onValueChanged.AddListener(OnSoundToggleValueChange);

    }
    private void HepticsToggleUpdate(bool music)
    {
        m_HepticsToggle.isOn = music;
        m_HepticsToggle.onValueChanged.RemoveAllListeners();
        m_HepticsToggle.onValueChanged.AddListener(OnHepticsToggleValueChange);
    }


    private void OnBackClick()
    {
        UIViewManager.ShowLast();
    }


    private void OnMusicToggleValueChange(bool value)
    {
        DataManager.Instance.IsMusicOn = value;
    }

    private void OnSoundToggleValueChange(bool value)
    {
        DataManager.Instance.IsSoundOn = value;
    }

    private void OnHepticsToggleValueChange(bool value)
    {
        DataManager.Instance.IsHepticsOn = value;
    }
}
