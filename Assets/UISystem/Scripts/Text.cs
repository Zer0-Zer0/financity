// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class Text : MonoBehaviour
    {
        public TextSO textData;
        public Style style;

        private TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            Setup();
            Configure();
        }

        public void Setup()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Configure()
        {
            textMeshProUGUI.color = textData.theme.GetTextColor(style);
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }

        private void OnValidate()
        {
            Init();
        }
    }
}