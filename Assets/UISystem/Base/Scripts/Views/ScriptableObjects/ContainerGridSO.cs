/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "ContainerGridSO", menuName = "UISystem/ContainerGridSO", order = 0)]
    public class ContainerGridSO : ScriptableObject
    {
        public RectOffset padding;
        public Vector2 spacing;
        public Vector2 cellSize;
    }
}