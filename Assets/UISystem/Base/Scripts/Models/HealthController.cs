/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float _startingHealth;

        [Header("Events")]
        public GameEvent onHealthChanged;

        private Health _health;

        private void Awake()
        {
            _health = new Health(_startingHealth);
        }

        private void Start()
        {
            onHealthChanged.Raise(this, _health.GetCurrentHealth());
        }

        public void OnHealthIncreased(Component sender, object data)
        {
            if (data is float amount)
            {
                _health.Increase(amount);
                onHealthChanged.Raise(this, _health.GetCurrentHealth());
            }
        }

        public void OnHealthDecreased(Component sender, object data)
        {
            if (data is float amount)
            {
                _health.Decrease(amount);
                onHealthChanged.Raise(this, _health.GetCurrentHealth());
            }
        }
    }
}