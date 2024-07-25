using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Toast : MonoBehaviour
{
    public static Toast s_Instance;

    private TMP_Text toast_Text;
    private CanvasGroup _canvasGroup;


    private Vector3 m_LocalPosition = Vector3.zero;

    void Start()
    {
        s_Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
        toast_Text = GetComponentInChildren<TMP_Text>();
        _canvasGroup.alpha = 0;
        m_LocalPosition = transform.localPosition;
    }
    public void Show(string message, float hideDelay = 3f)
    {
        toast_Text.text = message;
        _canvasGroup.alpha = 1;
        // ScaleAnimation();
        MoveAnimation();
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), hideDelay);
    }
    private void Hide()
    {
        // _canvasGroup.alpha = 0;
        ChangeAlphaValue(1f,0f);
    }
    private void ChangeAlphaValue(float  from, float to)
    {
        iTween.Stop(gameObject);
        var hashTable = iTween.Hash("time", 0.25f, "from", from, "to", to,
            "easetype", iTween.EaseType.linear, "onupdatetarget", gameObject, "onupdate", "OnUpdateAlphaValue");
        iTween.ValueTo(gameObject,hashTable);
    }
    
    private void OnUpdateAlphaValue(float value)
    {
        _canvasGroup.alpha = value;
    }
    
    private void ScaleAnimation()
    {
        transform.localScale = Vector3.one;
        var hashtable = iTween.Hash("scale",Vector3.zero,"time",0.25f,"easetype",iTween.EaseType.linear,"islocal",true);
        iTween.ScaleFrom(gameObject,hashtable);
    }

    private void MoveAnimation()
    {
        transform.localPosition = m_LocalPosition;
        var animationStartPosition = m_LocalPosition;
        animationStartPosition.y += 300;
        var hashtable = iTween.Hash("position",animationStartPosition,"time",0.25f,"easetype",iTween.EaseType.linear,"islocal",true);
        iTween.MoveFrom(gameObject,hashtable);
    }
}
