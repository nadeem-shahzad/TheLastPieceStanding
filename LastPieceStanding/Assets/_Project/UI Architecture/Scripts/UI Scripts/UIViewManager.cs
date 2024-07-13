using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIViewManager : MonoBehaviour
{
    private static UIViewManager s_Instance;

    [SerializeField] private UIView m_startingUIView;
    [SerializeField] private UIView[] m_allUIViews;


    private UIView m_CurrentUIView;

    private readonly Stack<UIView> m_history = new Stack<UIView>();


    private void Awake()
    {
        s_Instance = this;
    }

    private void Start()
    {
        foreach (var uiView in m_allUIViews)
        {
            uiView.Initialize();
            uiView.InitializeRequiredComponents();
            uiView.HideWithoutAnimation();
        }

        if (m_startingUIView != null)
            Show(m_startingUIView, true);
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

}
