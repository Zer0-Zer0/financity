using System.Collections;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(
        fileName = "DynamicSecuritySO",
        menuName = "Economy/DynamicSecuritySO",
        order = 0
    )]
    public class DynamicSecuritySO : ScriptableObject
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

        private int minTendency;
        private int maxTendency;

        private int tendencyAmount;
        private float tendency;

        [HideInInspector]
        public float currentValue;

        [HideInInspector]
        public float highShadow;

        [HideInInspector]
        public float lowShadow;

        [HideInInspector]
        public float openValue;

        [HideInInspector]
        public float closeValue;

        private const float trendPersistence = 0.7f;
        private const float meanReversionFactor = 0.05f;

        public void Init()
        {
            currentValue = openValue = lowShadow = initialValue;
            UpdateTendencyLimits();
        }

        public void Tick()
        {
            UpdateCurrentValue();
        }

        public void CloseTick()
        {
            CloseVolatileValue();
        }

        public void Reset()
        {
            ResetForNextTick();
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

        private void CloseVolatileValue()
        {
            closeValue = currentValue;
        }

        private void ResetForNextTick()
        {
            highShadow = lowShadow = openValue = currentValue;
        }

        private void UpdateTendencyLimits()
        {
            // Calculate min and max tendency based on instability
            if (instability <= 0)
            {
                minTendency = int.MaxValue; // or some large number
                maxTendency = int.MaxValue; // or some large number
            }
            else
            {
                minTendency = 1; // Minimum tendency when instability is 1
                maxTendency = Mathf.CeilToInt(1 / instability); // Maximum tendency based on instability
            }
        }
    }
}
