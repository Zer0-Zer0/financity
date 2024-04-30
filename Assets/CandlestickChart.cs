using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlestickChart : MonoBehaviour
{
    public CandleRender candleInstance;
    public List<CandleRender> candles = new List<CandleRender>();
    public Stock representedStock;

    public float lowest = -1f, highest = -1f;

    public float maxCandleHeight = 160f, maxcandleY = 200f;

    void Start()
    {
        representedStock.OnStockUpdate += HandleStockUpdate;
    }

    void RefreshcandleScale()
    {

        foreach (CandleRender candle in candles)
        {
            RectTransform candleTransform = candle.GetComponent<RectTransform>();

            //handling the candle height

            float candleHeight = candle.max - candle.min;
            float graphHeight = highest - lowest;

            float candleRatio = candleHeight / graphHeight;

            candleTransform.sizeDelta = new Vector2(candleTransform.sizeDelta.x, maxCandleHeight * candleRatio);

            //handling the candle Y position

            float candleYRatio = (((candle.min + candle.max) / 2) - lowest) / (highest - lowest);
            candle.transform.position = new Vector3(candle.transform.position.x, candleYRatio * maxcandleY, candle.transform.position.z);
            /*
            float candleScale, candleY;
            highLowRatio = lowest / highest;

            candleScale = highLowRatio;

            candleY = candle.min - lowest * maxcandleY;

            candleTransform.localScale = new Vector2(1f, candleScale);

            candle.transform.position = new Vector3(candle.transform.position.x, candle.transform.position.y + candleY, candle.transform.position.z);*/
        }
    }

    void HandleStockUpdate(string stockName, float open, float close, float high, float low)
    {
        if (lowest > low)
        {
            lowest = low;
        }
        if (highest < high)
        {
            highest = high;
        }

        if (lowest == -1f | highest == -1f)
        {
            lowest = low;
            highest = high;
        }
        // Do something with the updated stock data
        Debug.Log($"Received stock update - Stock: {stockName}, Open: ${open}, Close: ${close}, HighShadow: ${high}, LowShadow: ${low}");

        // Instantiate a new CandleRender and add it to the list
        CandleRender newCandle = Instantiate(candleInstance, new Vector3(candles.Count * 30f, transform.position.y, transform.position.z), transform.rotation);

        newCandle.open = open;
        newCandle.close = close;
        newCandle.min = low;
        newCandle.max = high;

        newCandle.transform.SetParent(transform);

        candles.Add(newCandle);

        RefreshcandleScale();
    }
}
