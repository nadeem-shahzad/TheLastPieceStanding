using System;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIView : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public abstract void Initialize();

    public virtual void InitializeRequiredComponents()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public virtual void HideWithoutAnimation()
    {
        gameObject.SetActive(false);
    }
    public virtual void Hide()
    {
        _canvasGroup.alpha = 1;
        ChangeAlphaValue(1, 0);
    }

    public virtual void Show()
    {
        _canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        ChangeAlphaValue(0,1);
    }
    private void ChangeAlphaValue(float  from, float to)
    {
        iTween.Stop(gameObject);
        bool activeState = from == 0;
        var hashTable = iTween.Hash("time", 0.5f, "from", from, "to", to, 
            "easetype", iTween.EaseType.linear,
            "onupdatetarget", gameObject, "onupdate", "OnUpdateAlphaValue",
            "oncompletetarget",gameObject,"oncomplete","ChangeActiveState", "oncompleteparams", activeState,
            "ignoretimescale", true);
        iTween.ValueTo(gameObject,hashTable);
    }

    private void OnUpdateAlphaValue(float value)
    {
        _canvasGroup.alpha = value;
    }

    private void ChangeActiveState(bool value)
    {
        gameObject.SetActive(value);
    }
}
