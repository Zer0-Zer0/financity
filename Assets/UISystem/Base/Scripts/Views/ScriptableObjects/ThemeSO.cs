/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(menuName = "UISystem/ThemeSO", fileName = "ThemeSO")]
    public class ThemeSO : ScriptableObject
    {
        [Header("Primary")]
        public Color primaryBG;
        public Color primaryText;

        [Header("Secondary")]
        public Color secondaryBG;
        public Color secondaryText;

        [Header("Tertiary")]
        public Color tertiaryBG;
        public Color tertiaryText;

        [Header("Other")]
        public Color disabled;

        public Color GetBackgroundColor(Style style)
        {
            switch (style)
            {
                case Style.Primary:
                    return primaryBG;
                case Style.Secondary:
                    return secondaryBG;
                case Style.Tertiary:
                    return tertiaryBG;
                default:
                    Debug.LogError("ERROR: Non implemented style");
                    return disabled;
            }
        }

        public Color GetTextColor(Style style)
        {
            switch (style)
            {
                case Style.Primary:
                    return primaryText;
                case Style.Secondary:
                    return secondaryText;
                case Style.Tertiary:
                    return tertiaryText;
                default:
                    Debug.LogError("ERROR: Non implemented style");
                    return disabled;
            }
        }
    }
}