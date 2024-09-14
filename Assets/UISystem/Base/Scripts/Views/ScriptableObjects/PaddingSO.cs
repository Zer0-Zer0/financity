/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "Padding_", menuName = "ScriptableObjects/UISystem/Padding")]
    public class PaddingSO : ScriptableObject
    {
        public RectOffset padding;
        public float spacing;
    }
}