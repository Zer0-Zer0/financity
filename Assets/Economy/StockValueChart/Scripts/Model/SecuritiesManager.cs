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
        private int tickIntervalToClose = 4;
        private int ticksRemainingToClose;
        private float updateInterval = 1.0f;

        private float updateTimer;

        private void Start()
        {
            foreach (var security in securities)
                security.Init();

            ticksRemainingToClose = tickIntervalToClose;

            OnEconomyTick.Raise(this, securities);
        }

        private void Update()
        {
            updateTimer += Time.unscaledDeltaTime;
            if (updateTimer >= updateInterval)
            {
                UpdateSecurities();
                updateTimer = 0f;
            }
        }

        private void UpdateSecurities()
        {
            foreach (var security in securities)
                security.Tick();

            ticksRemainingToClose--;

            if (ticksRemainingToClose == 0)
            {
                foreach (var security in securities)
                    security.CloseTick();
                ticksRemainingToClose = tickIntervalToClose;
                HandleStockUpdate();
                foreach (var security in securities)
                    security.Reset();
            }
        }

        private void HandleStockUpdate()
        {
            foreach (var security in securities)
                /* Debug.Log(
                     $"Stock Update: Open: {security.openValue}, Close: {security.closeValue}, High: {security.highShadow}, Low: {security.lowShadow}"
                 );*/
                OnEconomyTick.Raise(this, securities);
        }
    }
}
