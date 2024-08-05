using System.Collections;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(
        fileName = "FluctuatingValueSO",
        menuName = "Economy/FluctuatingValueSO",
        order = 0
    )]
    public class FluctuatingValueSO : ScriptableObject
    {
        [Header("Initial Values")]
        [SerializeField]
        private float initialValue;

        [SerializeField]
        private float instability;

        [SerializeField]
        private float maxValue;

        [SerializeField]
        private float minValue;

        [Header("Tendency Settings")]
        [SerializeField]
        private int minTendency = 1;

        [SerializeField]
        private int maxTendency = 1;

        private int tendencyAmount;
        private float tendency;

        [HideInInspector]
        public float currentValue;

        [HideInInspector]
        public float currentDelay;

        [HideInInspector]
        public float highShadow;

        [HideInInspector]
        public float lowShadow;

        [HideInInspector]
        public float openValue;

        [HideInInspector]
        public float closeValue;

        private float initialDelay;
        private const float trendPersistence = 0.7f;
        private const float meanReversionFactor = 0.05f;

        public void Init(float delay)
        {
            currentValue = openValue = lowShadow = initialValue;
            currentDelay = delay;
            initialDelay = delay;
        }

        public void Tick()
        {
            if (currentDelay > 0)
            {
                UpdateCurrentValue();
                currentDelay -= Time.fixedDeltaTime;
            }
            else
            {
                CloseFloatingValue();
                ResetForNextTick();
            }
        }

        private void UpdateCurrentValue()
        {
            if (tendencyAmount > 0)
            {
                ApplyTendency();
                tendencyAmount--;
            }
            else
            {
                SetNewTendency();
            }
        }

        private void ApplyTendency()
        {
            float tendencyPercentage = Random.Range(0.0001f, 1.0f);
            float trendFactor = 1 + trendPersistence * (tendency - 1);

            currentValue +=
                currentValue * instability * tendency * tendencyPercentage * trendFactor;

            // Mean reversion
            float meanReversion = (initialValue - currentValue) * meanReversionFactor;
            currentValue += meanReversion;

            // Clamp the stock value
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

            // Update shadows
            highShadow = Mathf.Max(highShadow, currentValue);
            lowShadow = Mathf.Min(lowShadow, currentValue);
        }

        private void SetNewTendency()
        {
            tendency = Random.Range(-1.0f, 1.0f);
            tendencyAmount = Random.Range(minTendency, maxTendency);
        }

        private void CloseFloatingValue()
        {
            closeValue = currentValue;
        }

        private void ResetForNextTick()
        {
            highShadow = lowShadow = openValue = currentValue;
            currentDelay = initialDelay;
        }
    }
}
