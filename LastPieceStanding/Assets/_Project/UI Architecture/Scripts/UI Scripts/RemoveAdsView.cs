using System.Collections;
using System.Collections.Generic;
using _Project.UI_Architecture.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class RemoveAdsView : PopUp
{

    [SerializeField] private Button m_CrossButton;
    
    public override void Initialize()
    {
        m_CrossButton.onClick.AddListener(()=>
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
            UIViewManager.HidePopUp();
        });
    }


    public void RemoveAllAds()
    {
        // Implement Remove ADs Check
    }
}
