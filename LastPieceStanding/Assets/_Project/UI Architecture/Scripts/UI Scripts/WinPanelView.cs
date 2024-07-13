﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanelView : UIView
{
    [SerializeField] private Button m_RestartButton;
    public override void Initialize()
    {
        m_RestartButton.onClick.AddListener(() => OnRestart());
    }



    private void OnWin()
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.StageComplete);
        this.Show();
        Time.timeScale = 0f;
    }


    public void OnRestart()
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.Click);

        SceneManager.LoadScene("Gameplay");
    }
}
