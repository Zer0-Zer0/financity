/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        public ThemeSO OverwriteTheme;
        private void Awake()
        {
            Init();
        }

        protected abstract void Setup();
        protected abstract void Configure();

        private void Init()
        {
            Setup();
            Configure();
        }

        private void OnValidate()
        {
            Init();
        }

        protected ThemeSO GetMainTheme()
        {
            if (OverwriteTheme != null)
                return OverwriteTheme;
            else if (ThemeManager.Instance != null)
                return ThemeManager.Instance.GetMainTheme();
            return null;
        }
    }
}