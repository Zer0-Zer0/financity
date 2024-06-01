using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newCandlestickChart : MonoBehaviour
{
    public Canvas canvas;
    public CandleRender candlePrefab;
    public float CandlePadding = 100f;
    public int maxCandles = 5;
    public List<CandleRender> candles = new List<CandleRender>();
    public Stock representedStock;
    public float lowest = -1f, highest = 1f;
    public float maxCandleHeight = 160f, maxCandleY = 200f;

    void Start()
    {
        representedStock.OnStockUpdate += HandleStockUpdate;
    }

    CandleRender InstantiateCandle()
    {
        CandleRender newCandle = Instantiate(candlePrefab, canvas.transform);
        candles.Add(newCandle);

        Arrangecandles();
        return newCandle;
    }

    void Arrangecandles()
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
        candles.RemoveAt(0);
        Destroy(oldestCandle);
    }

    void RefreshCandleScale()
    {
        foreach (CandleRender candle in candles)
        {
            RectTransform candleTransform = candle.GetComponent<RectTransform>();

            float candleHeight = candle.max - candle.min;
            float graphHeight = highest - lowest;
            float candleRatio = candleHeight / graphHeight;
            float candleYRatio = (((candle.min + candle.max) / 2) - lowest) / (highest - lowest);

            candleTransform.sizeDelta = new Vector2(candleTransform.sizeDelta.x, candleYRatio * candleRatio);
            Arrangecandles();
            //candle.transform.position = new Vector3(candle.transform.position.x, candleYRatio * maxCandleY, candle.transform.position.z);
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

        if (lowest == -1f || highest == -1f)
        {
            lowest = low;
            highest = high;
        }

        //CandleRender newCandle = Instantiate(candlePrefab, canvas.transform);

        CandleRender newCandle = InstantiateCandle();

        newCandle.open = open;
        newCandle.close = close;
        newCandle.min = low;
        newCandle.max = high;

        //newCandle.SetParent(canvas);

        //candles.Add(newCandle);

        RefreshCandleScale();
    }
}