using System.Collections;
using System.Collections.Generic;
using _Project.UI_Architecture.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class RateUsPopup : PopUp
{

    [SerializeField] private Button m_BackButton;
    [SerializeField] private Button m_ActionButton;
    
    public override void Initialize()
    {
        m_BackButton.onClick.AddListener(() =>
        {
            UIViewManager.HidePopUp();
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        });
        
        m_ActionButton.onClick.AddListener(RateUs);
    }
    
    private void RateUs()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        Application.OpenURL(Constants.ApplicationLink);
        DataManager.Instance.IsRated = true;
    }
}
