using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(
        fileName = "DynamicSecurity_",
        menuName = "ScriptableObjects/Economy/DynamicSecurity"
    )]
    public class DynamicSecuritySO : ScriptableObject
    {
        [Header("Initial Values")]
        [SerializeField]
        private float initialValue;

        [SerializeField, Range(0f, 1f)]
        private float instability;

        [SerializeField]
        private float maxValue;

        [SerializeField]
        private float minValue;

        private int maxTrendCount;
        private int remainingCurrentTrend;
        private float trend;

        [HideInInspector]
        public float currentValue;

        [HideInInspector]
        public float historicalMaximum;

        [HideInInspector]
        public float historicalMinimum;

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
            ResetValues();
            InitHistoricalValues();
            UpdateTrendLimits();
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
            ResetValues();
        }

        private void UpdateCurrentValue()
        {
            if (remainingCurrentTrend > 0)
            {
                ApplyTrend();
                remainingCurrentTrend--;
            }
            else
            {
                SetNewTrend();
            }
        }

        private void ApplyTrend()
        {
            float trendPercentage = UnityEngine.Random.Range(0.0001f, 1.0f);
            float trendFactor = 1 + trendPersistence * (trend - 1);

            currentValue += currentValue * instability * trend * trendPercentage * trendFactor;

            // Mean reversion
            float meanReversion = (initialValue - currentValue) * meanReversionFactor;
            currentValue += meanReversion;

            // Clamp the stock value
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

            // Update shadows
            highShadow = Mathf.Max(highShadow, currentValue);
            lowShadow = Mathf.Min(lowShadow, currentValue);

            historicalMaximum = Mathf.Max(historicalMaximum, highShadow);
            historicalMinimum = Mathf.Min(historicalMinimum, lowShadow);
        }

        private void SetNewTrend()
        {
            trend = UnityEngine.Random.Range(-1.0f, 1.0f);
            remainingCurrentTrend = UnityEngine.Random.Range(1, maxTrendCount);
        }

        private void CloseVolatileValue()
        {
            closeValue = currentValue;
        }

        private void ResetValues()
        {
            highShadow = lowShadow = openValue = currentValue = initialValue;
        }

        private void InitHistoricalValues()
        {
            historicalMaximum = historicalMinimum = currentValue;
        }

        private void UpdateTrendLimits()
        {
            if (instability <= 0)
                throw new ArgumentOutOfRangeException(
                    "ERROR: Instability can't be lower than Zero"
                );
            else
                maxTrendCount = Mathf.CeilToInt(Mathf.Pow(2, (1 - instability) * 2));
        }
    }
}
