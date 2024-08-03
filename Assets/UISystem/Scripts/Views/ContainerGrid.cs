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
    public class ContainerGrid : CustomUIComponent
    {
        public ContainerGridSO gridData;
        private GridLayoutGroup gridLayoutGroup;
        protected override void Setup()
        {
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }
        protected override void Configure()
        {
            gridLayoutGroup.padding = gridData.padding;
            gridLayoutGroup.spacing = gridData.spacing;
            gridLayoutGroup.cellSize = gridData.cellSize;
        }
    }
}