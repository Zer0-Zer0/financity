// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    [CreateAssetMenu(menuName = "CustomUI/TextSO", fileName = "TextSO")]
    public class TextSO : ScriptableObject
    {
        public ThemeSO theme;

        public TMP_FontAsset font;
        public float size;
    }
}