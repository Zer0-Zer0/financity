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
    public class DemoButtonViewModel : MonoBehaviour
    {
        [SerializeField] private CustomButton _button;

        [Header("Event")]
        [SerializeField] private GameEvent _onClickEvent;

        [Header("EventData")]
        [SerializeField] private float _data;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClickEvent.Raise(this, _data);
        }
    }
}