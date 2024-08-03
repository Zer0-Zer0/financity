using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "CustomUI/SliderSO", menuName = "SliderSO", order = 0)]
    public class SliderSO : ScriptableObject
    {
        public bool interactable;
        public Color backgroundColor;
        public Color fillColor;
        public float min;
        public float max;
    }
}