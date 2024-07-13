using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class StartPanelView : UIView
{
    [SerializeField] private Button m_TapToStartButton;
    [SerializeField] private UnityEvent m_OnTapToStartEvent;

    public override void Initialize()
    {
        m_TapToStartButton.onClick.AddListener(TapToStart);
    }

    private void TapToStart()
    {

        UIViewManager.Show<GamePlayPanelView>();
        m_OnTapToStartEvent?.Invoke();
    }
}
