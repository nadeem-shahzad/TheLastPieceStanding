using UnityEngine;

namespace _Project.UI_Architecture.Scripts.UI_Scripts
{
    public abstract class Views : MonoBehaviour
    {
        public abstract void Initialize();

        public abstract void Show();
        public abstract void Hide();
        public abstract void HideWithoutAnimation();


    }
}