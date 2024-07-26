using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPanel : UIView
{
    [SerializeField] private Button m_NextButton;
    [SerializeField] private TMP_Text m_NextButtonText;
    [SerializeField] private GameObject[] m_MiniPanels;

    private int m_Index = 0;
    public override void Initialize()
    {
        m_NextButton.onClick.AddListener(OnNextClick);
        OnNextClick();
    }

    private void OnNextClick()
    {
        if (m_Index != 0)
            SoundManager.Instance.PlaySound(SoundManager.SoundType.Click);
        
        
        if (m_Index >= m_MiniPanels.Length )
        {
            UIViewManager.GetUIView<LoadingPanelView>().IsTutorialDone = true;
            SceneManager.LoadScene("Gameplay");
        }
        else
        {
            m_MiniPanels[m_Index].SetActive(true);
            m_Index++;
            
            if (m_Index == (m_MiniPanels.Length))
                m_NextButtonText.text = "Let's Start!";
        }
    }
}
