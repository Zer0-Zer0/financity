/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;
namespace UISystem
{
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField] private ThemeSO mainTheme;

        private static ThemeManager _instance;
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThemeManager();
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public ThemeSO GetMainTheme()
        {
            return mainTheme;
        }
    }
}