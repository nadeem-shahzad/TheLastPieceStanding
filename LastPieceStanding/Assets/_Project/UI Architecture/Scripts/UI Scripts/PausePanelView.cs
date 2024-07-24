using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelView : UIView
{
    [SerializeField] private Button m_SettingButton;
    [SerializeField] private Button m_LeaveButton;
    [SerializeField] private Button m_ResumeButton;



    public override void Initialize()
    {
        m_LeaveButton.onClick.AddListener(OnLeaveClick);
        m_ResumeButton.onClick.AddListener(OnResumeClick);
        m_SettingButton.onClick.AddListener(OnSettingClick);
    }



    private void OnResumeClick()
    {
        UIViewManager.ShowLast();
    }

    private void OnSettingClick()
    {
        UIViewManager.ShowPopUp<SettingPanelView>();
    }

    private void OnLeaveClick()
    {
        
    }
}
