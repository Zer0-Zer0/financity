// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
using UnityEngine;

namespace UISystem
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        protected abstract void Setup();
        protected abstract void Configure();

        private void Init()
        {
            Setup();
            Configure();
        }

        private void OnValidate()
        {
            Init();
        }
    }
}