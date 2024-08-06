/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public abstract class GenericButtonViewModel<T> : MonoBehaviour
    {
        [SerializeField] private CustomButton _button;

        [Header("Event")]
        [SerializeField] private GameEvent _onClickEvent;

        [Header("EventData")]
        public T _data;

        private void OnEnable()
        {
            _button.onClickEvent.AddListener(OnClickMethod);
        }

        private void OnDisable()
        {
            _button.onClickEvent.RemoveListener(OnClickMethod);
        }

        private void OnClickMethod()
        {
            _onClickEvent.Raise(this, _data);
        }
    }
}