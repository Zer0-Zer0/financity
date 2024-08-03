/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class View : CustomUIComponent
    {
        public ViewSO viewData;

        public GameObject containerTop;

        public GameObject containerCenter;
        public GameObject containerBottom;

        private Image imageTop;
        private Image imageCenter;
        private Image imageBottom;

        private VerticalLayoutGroup verticalLayoutGroup;

        protected override void Setup()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            imageTop = containerTop.GetComponent<Image>();
            imageCenter = containerCenter.GetComponent<Image>();
            imageBottom = containerBottom.GetComponent<Image>();
        }

        protected override void Configure()
        {
            ThemeSO theme = GetMainTheme();
            if (theme == null) throw new Exception("ERROR: ThemeManager missing or not found and no override theme added.");

            verticalLayoutGroup.padding = viewData.padding;
            verticalLayoutGroup.spacing = viewData.spacing;

            imageTop.color = theme.primaryBG;
            imageCenter.color = theme.secondaryBG;
            imageBottom.color = theme.tertiaryBG;
        }
    }
}