/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(menuName = "UISystem/PaddingSO", fileName = "PaddingSO")]
    public class PaddingSO : ScriptableObject
    {
        public RectOffset padding;
        public float spacing;
    }
}