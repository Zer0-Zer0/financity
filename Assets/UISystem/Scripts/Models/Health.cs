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
    public class Health
    {
        private float _startingHealth;
        private float _currentHealth;

        public Health(float startingHealth)
        {
            this._startingHealth = startingHealth;
            Reset();
        }

        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void Reset()
        {
            _currentHealth = _startingHealth;
        }

        public void Increase(float amount)
        {
            _currentHealth += Mathf.Abs(amount);
        }

        public void Decrease(float amount)
        {
            _currentHealth -= Mathf.Abs(amount);
        }
    }
}