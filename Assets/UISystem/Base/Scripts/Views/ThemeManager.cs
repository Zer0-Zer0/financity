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
        public static ThemeManager Instance { get; private set; }

        private void OnValidate()
        {
            Init();
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            Instance = this;
        }

        public ThemeSO GetMainTheme()
        {
            return mainTheme;
        }
    }
}