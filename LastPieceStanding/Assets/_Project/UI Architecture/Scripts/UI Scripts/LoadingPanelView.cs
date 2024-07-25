using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingPanelView : UIView
{
    [SerializeField] private Color _backGroundCColor;
    [SerializeField] private Image _background;
    [SerializeField] private Image _studioIcon;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private float _loadingTime;
    [SerializeField] private Text _loadingText;
    [SerializeField] private UnityEvent _OnCompleteLoading;
    [SerializeField] private UIView _NextView;
    [SerializeField] private UIView m_TutorialView;

    

    private float targetValue;
    private float currentValue;
    private float timer;
    
    private bool IsTutorialDone
    {
        get => PlayerPrefs.GetInt(Constants.TutorialKey, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(Constants.TutorialKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public override void Initialize()
    {
        _background.color = _backGroundCColor;
        if(_studioIcon.sprite == null)
            _studioIcon.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        currentValue = 0;
        targetValue = 100;
        StartCoroutine(Loading(_OnCompleteLoading));
    }

    IEnumerator Loading(UnityEvent onComplete)
    {
        while (currentValue < targetValue)
        {
            timer += Time.deltaTime;
            currentValue = Mathf.Lerp(0f, targetValue, timer / _loadingTime);
            _loadingBar.value = currentValue;
            _loadingText.text = currentValue.ToString("F2") + " %";
            yield return new WaitForEndOfFrame();
        }

        // Ensure the slider reaches exactly 100%
        _loadingBar.value = targetValue;
        _loadingText.text = targetValue + " %";
        onComplete?.Invoke();
    
        if (_NextView != null && IsTutorialDone)
            UIViewManager.Show(_NextView,true);
        
        if (m_TutorialView != null && IsTutorialDone is false)
        {
            UIViewManager.Show(m_TutorialView, true);
            IsTutorialDone = true;
        }
        
    }
    
    
}
