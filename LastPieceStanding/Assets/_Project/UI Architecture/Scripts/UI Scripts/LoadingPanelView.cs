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
    

    private float targetValue;
    private float currentValue;
    private float timer;
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
            yield return null;
        }

        // Ensure the slider reaches exactly 100%
        _loadingBar.value = targetValue;
        _loadingText.text = targetValue + " %";
        onComplete?.Invoke();
    
        if (_NextView != null)
            UIViewManager.Show(_NextView,true);
    }
    
    
}
