using System.Collections;
using UnityEngine;

[CreateAssetMenu(
    fileName = "FluctuatingValueSO",
    menuName = "Economy/FluctuatingValueSO",
    order = 0
)]
public class FluctuatingValueSO : ScriptableObject
{
    [HideInInspector]
    public float currentValue,
        currentDelay;

    [Header("Initial Values")]
    [SerializeField]
    private float initialValue;

    [SerializeField]
    private float instability;

    [SerializeField]
    private float delay;

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


    private float highShadow;
    private float lowShadow;
    private float openValue;
    private float closeValue;
    private float tendency;

    private const float trendPersistence = 0.7f;
    private const float meanReversionFactor = 0.05f;


    public delegate void StockUpdateEventHandler(
        float open,
        float close,
        float high,
        float low
    );
    public event StockUpdateEventHandler OnStockUpdate;

    public void Init()
    {
        currentValue = openValue = lowShadow = initialValue;
        currentDelay = delay;
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
            CloseStockValue();
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
        currentValue += currentValue * instability * tendency * tendencyPercentage * trendFactor;

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

    private void CloseStockValue()
    {
        closeValue = currentValue;
        OnStockUpdate?.Invoke(openValue, currentValue, highShadow, lowShadow);
    }

    private void ResetForNextTick()
    {
        highShadow = lowShadow = openValue = currentValue;
        currentDelay = delay;
    }
}
