using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WinPanelView : UIView
{
    [SerializeField] private Button m_RestartButton;
    [SerializeField] private Transform m_Emojies;

    [Header("Animation Related Things")] 
    [SerializeField] private GameObject m_UpperBar;
    [SerializeField] private GameObject[] m_BottomObjects;
    
    public override void Initialize()
    {
        m_RestartButton.onClick.AddListener(OnRestart);
        UIEvents.a_OnGameWin = null;
        UIEvents.a_OnGameWin += OnWin;
    }



    private void OnWin()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.LevelUp);
        ShowEmoji();
        UIViewManager.Show(this,true);
        PanelAnimations();
    }

    private void ShowEmoji()
    {
        var index = Random.Range(0, m_Emojies.childCount);
        m_Emojies.GetChild(index).gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        // SoundManager.instance.PlaySound(SoundManager.SoundType.Click);
        LevelManager.Level++;
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
        UIEvents.a_OnGameWin -= OnWin;
    }
    
}
