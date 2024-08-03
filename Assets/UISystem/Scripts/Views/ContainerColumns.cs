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
    public class ContainerColumns : CustomUIComponent
    {
        public PaddingSO paddingData;
        private HorizontalLayoutGroup horizontalLayoutGroup;
        protected override void Setup()
        {
            horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        }
        protected override void Configure()
        {
            horizontalLayoutGroup.padding = paddingData.padding;
            horizontalLayoutGroup.spacing = paddingData.spacing;
        }
    }
}