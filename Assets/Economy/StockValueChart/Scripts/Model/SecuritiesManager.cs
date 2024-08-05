using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class SecuritiesManager : MonoBehaviour
    {
        [Header("Securities")]
        [SerializeField]
        private List<DynamicSecuritySO> securities;

        [Header("Game Event")]
        [SerializeField]
        private GameEvent OnEconomyTick;

        [Header("Update frequency")]
        [SerializeField]
        private float updateInterval = 1.0f;

        private float updateTimer;

        private void Start()
        {
            foreach (var security in securities)
            {
                security.Init(updateInterval);
            }
            OnEconomyTick.Raise(this, securities);
        }

        private void Update()
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= updateInterval)
            {
                UpdateSecurities();
                updateTimer = 0f;
            }
        }

        private void UpdateSecurities()
        {
            foreach (var security in securities)
            {
                security.Tick();
            }
        }

        private void HandleStockUpdate(float open, float close, float high, float low)
        {
            Debug.Log($"Stock Update: Open: {open}, Close: {close}, High: {high}, Low: {low}");
        }
    }
}
