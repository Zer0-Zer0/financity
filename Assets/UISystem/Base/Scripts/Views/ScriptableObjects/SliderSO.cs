/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "Slider_", menuName = "ScriptableObjects/UISystem/Slider")]
    public class SliderSO : ScriptableObject
    {
        public bool interactable;
        public Color backgroundColor;
        public Color fillColor;
        public float min;
        public float max;
    }
}