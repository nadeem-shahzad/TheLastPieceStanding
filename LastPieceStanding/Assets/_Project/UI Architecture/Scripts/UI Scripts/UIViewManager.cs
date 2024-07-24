using System.Collections.Generic;
using _Project.UI_Architecture.Scripts.UI_Scripts;
using UnityEngine;

public class UIViewManager : MonoBehaviour
{
    private static UIViewManager s_Instance;

    [SerializeField] private UIView m_startingUIView;
    [SerializeField] private UIView m_GameUIView;
    [SerializeField] private UIView[] m_allUIViews;
    [SerializeField] private PopUp[] m_allPopUps;

    private UIView m_CurrentUIView;

    private PopUp m_CurrentPopUp;
    private readonly Stack<UIView> m_history = new Stack<UIView>();

    private static bool IsFirstTime = true;


    private void Awake()
    {
        s_Instance = this;
    }

    private void Start()
    {
        UIEvents.a_OnGameWin = null;
        UIEvents.a_OnGameLose= null;


        UIEvents.a_DeleteSelectedCard= null;
        UIEvents.a_UpdateCoins= null;
        
        foreach (var uiView in m_allUIViews)
        {
            uiView.Initialize();
            uiView.InitializeRequiredComponents();
            uiView.HideWithoutAnimation();
        }

        foreach (PopUp m_PopUp in m_allPopUps)
        {
            m_PopUp.Initialize();
            m_PopUp.InitializeRequiredComponents();
            m_PopUp.HideWithoutAnimation();
        }

        if (m_startingUIView != null && IsFirstTime)
        {
            IsFirstTime = false;
            Show(m_startingUIView, true);
        }
        
        if (m_GameUIView != null && !IsFirstTime)
            Show(m_GameUIView, true);
        
        CurrencyManager.Instance.Coins = CurrencyManager.Instance.Coins;
    }


    public static T GetUIView<T>() where T : UIView
    {
        for (var i = 0; i < s_Instance.m_allUIViews.Length; i++)
        {
            if (s_Instance.m_allUIViews[i] is T tUIView)
            {
                return tUIView;
            }
        }

        return null;
    }


    public static void Show<T>(bool _remeber = true) where T : UIView
    {
        for (int i = 0; i < s_Instance.m_allUIViews.Length; i++)
        {
            if (s_Instance.m_allUIViews[i] is T tUIView)
            {
                if (s_Instance.m_CurrentUIView != null)
                {
                    if (_remeber)
                    {
                        s_Instance.m_history.Push(s_Instance.m_CurrentUIView);
                    }
                    s_Instance.m_CurrentUIView.Hide();
                }
                s_Instance.m_allUIViews[i].Show();
                s_Instance.m_CurrentUIView = s_Instance.m_allUIViews[i];
            }
        }
    }


    public static void Show(UIView _uIView, bool _remeber)
    {
        if (s_Instance.m_CurrentUIView != null)
        {
            if (_remeber)
            {
                s_Instance.m_history.Push(s_Instance.m_CurrentUIView);
            }
            s_Instance.m_CurrentUIView.Hide();
        }
        _uIView.Show();
        s_Instance.m_CurrentUIView = _uIView;
    }

    public static void ShowLast()
    {
        if (s_Instance.m_history.Count != 0)
        {
            Show(s_Instance.m_history.Pop(), false);
        }
    }

    
    #region Pop Ups
    public static void ShowPopUp<T>() where T : PopUp
    {
        for (int i = 0; i < s_Instance.m_allPopUps.Length; i++)
        {
            if (s_Instance.m_allPopUps[i] is T tUIView)
            {
                HidePopUp();
                s_Instance.m_allPopUps[i].Show();
                s_Instance.m_CurrentPopUp = s_Instance.m_allPopUps[i];
                GameManager.Instance.IsGameEnded = true;
            }
        }
    }
    
    public static void ShowPopUp(PopUp popUp)
    {
        HidePopUp();
        popUp.Show();
        s_Instance.m_CurrentPopUp = popUp;
    }
    
    public static void HidePopUp()
    {
        if (s_Instance.m_CurrentPopUp == null) return;
    
        s_Instance.m_CurrentPopUp.Hide();
        GameManager.Instance.IsGameEnded = false;
        s_Instance.m_CurrentPopUp = null;
    }

    #endregion


}
