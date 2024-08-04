/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "SliderSO", menuName = "UISystem/SliderSO", order = 0)]
    public class SliderSO : ScriptableObject
    {
        public bool interactable;
        public Color backgroundColor;
        public Color fillColor;
        public float min;
        public float max;
    }
}