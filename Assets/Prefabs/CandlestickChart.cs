using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandlestickChart : MonoBehaviour
{
    public CandleRender candlePrefab;
    public List<CandleRender> candles = new List<CandleRender>();
    public Stock representedStock;

    public float CandlePadding = 30f;
    public int maxCandles = 25;

    public float lowest = -1f, highest = -1f;
    public float maxCandleHeight = 160f, maxCandleY = 200f;

    void Start()
    {
        representedStock.OnStockUpdate += HandleStockUpdate;
    }

    void RefreshCandleScale()
    {
        ArrangeCandles();
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
            candleTransform.anchoredPosition = new Vector2(candleTransform.anchoredPosition.x, candleTransform.anchoredPosition.y + candleYRatio * maxCandleY);
        }
        
    }

    CandleRender InstantiateCandle()
    {
        CandleRender newCandle = Instantiate(candlePrefab, transform.position, transform.rotation);
        candles.Add(newCandle);

        ArrangeCandles();
        return newCandle;
    }

    void ArrangeCandles()
    {
        float totalWidth = candles.Count * CandlePadding;

        if (totalWidth > maxCandles * CandlePadding)
        {
            DestroyOldestCandle();
        }

        for (int i = 0; i < candles.Count; i++)
        {
            RectTransform CandleTransform = candles[i].GetComponent<RectTransform>();
            CandleTransform.anchoredPosition = new Vector2(i * CandlePadding, 0);
        }
    }

    void DestroyOldestCandle()
    {
        CandleRender oldestCandle = candles[0];
        Destroy(oldestCandle);
        candles.RemoveAt(0);
        oldestCandle.gameObject.SetActive(false);
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
        CandleRender newCandle = InstantiateCandle();

        newCandle.open = open;
        newCandle.close = close;
        newCandle.min = low;
        newCandle.max = high;

        newCandle.transform.SetParent(transform);

        RefreshCandleScale();
    }
}
