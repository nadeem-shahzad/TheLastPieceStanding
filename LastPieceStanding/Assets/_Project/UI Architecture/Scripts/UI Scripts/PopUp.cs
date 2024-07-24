using UnityEngine;

namespace _Project.UI_Architecture.Scripts.UI_Scripts
{
    public abstract class PopUp : Views
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject m_Panel;
        public void InitializeRequiredComponents()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
        }
    
        public override void HideWithoutAnimation()
        {
            m_Panel.transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }
        public override void Hide()
        {
            var hashtable = iTween.Hash("scale", Vector3.zero, "time", 0.25f, "easetype", iTween.EaseType.spring,"islocal",true,
                "oncompletetarget",gameObject, "oncomplete","ChangeActiveState");
            iTween.ScaleTo(m_Panel,hashtable);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            var hashtable = iTween.Hash("scale", Vector3.one, "time", 0.25f, "easetype", iTween.EaseType.spring,"islocal",true);
            iTween.ScaleTo(m_Panel,hashtable);
        }


        private void ChangeActiveState()
        {
            gameObject.SetActive(false);
        }
        

    }
}