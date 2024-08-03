/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class ContainerRows : CustomUIComponent
    {
        public ViewSO viewData;
        private VerticalLayoutGroup verticalLayoutGroup;
        protected override void Setup()
        {
            verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        }
        protected override void Configure()
        {
            verticalLayoutGroup.padding = viewData.padding;
            verticalLayoutGroup.spacing = viewData.spacing;
        }
    }
}