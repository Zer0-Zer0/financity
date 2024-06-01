using System.Collections;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Rendering;
using UnityEngine;

public class Stock : MonoBehaviour
{
    public string stockName = "Stock";
    
    [HideInInspector] public float currentValue, currentDelay;
    [SerializeField] private float initialValue, instability, delay, maxValue, minValue;


    [SerializeField] private int minTendency = 1, maxTendency = 1;
    int tendencyAmount;

    private float highShadow, lowShadow, openValue, closeValue, tendency;

    private float trendPersistence = 0.7f;
    private float meanReversionFactor = 0.05f;

    // Define a delegate for the stock update event
    public delegate void StockUpdateEventHandler(string stockName, float open, float close, float high, float low);
    // Declare the event using the above delegate
    public event StockUpdateEventHandler OnStockUpdate;

    void Start()
    {
        currentValue = openValue = lowShadow = initialValue;
        currentDelay = delay;
        //instability = Mathf.Sqrt(instability); Não ative :(
    }
    private void FixedUpdate()
    {
        if (currentDelay > 0)
        {
            if (tendencyAmount > 0)
            {
                float tendencyPercentage = Random.Range(0.0001f, 1.0f);

                // Introduce trend persistence
                float trendFactor = 1 + trendPersistence * (tendency - 1);
                currentValue += currentValue * instability * tendency * tendencyPercentage * trendFactor;

                // Introduce mean reversion
                float meanReversion = (initialValue - currentValue) * meanReversionFactor;
                currentValue += meanReversion;

                // Clamp the stock value to avoid unrealistic values
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

                if (highShadow < currentValue)
                {
                    highShadow = currentValue;
                }
                if (lowShadow > currentValue)
                {
                    lowShadow = currentValue;
                }

                tendencyAmount--;
            }
            else
            {
                tendency = Random.Range(-1.0f, 1.0f);
                tendencyAmount = Random.Range(minTendency, maxTendency);
            }
            currentDelay -= Time.fixedDeltaTime;
        }
        else
        {
            closeValue = currentValue;
            // Invoke the stock update event with the current stock data
            OnStockUpdate?.Invoke(stockName, openValue, currentValue, highShadow, lowShadow);
            highShadow = lowShadow = openValue = currentValue;
            currentDelay = delay;
        }
    }
}
