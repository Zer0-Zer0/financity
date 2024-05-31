using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject CandlePrefab;
    public float CandleWidth = 100f;
    public int maxCandles = 5;

    private List<GameObject> candles = new List<GameObject>();

    void InstantiateCandle()
    {
        GameObject newCandle = Instantiate(CandlePrefab, canvas.transform);
        candles.Add(newCandle);

        Arrangecandles();
    }

    void Arrangecandles()
    {
        float totalWidth = candles.Count * CandleWidth;

        if (totalWidth > maxCandles * CandleWidth)
        {
            DestroyOldestCandle();
        }

        for (int i = 0; i < candles.Count; i++)
        {
            RectTransform CandleTransform = candles[i].GetComponent<RectTransform>();
            CandleTransform.anchoredPosition = new Vector2(i * CandleWidth, 0);
        }
    }

    void DestroyOldestCandle()
    {
        GameObject oldestCandle = candles[0];
        candles.RemoveAt(0);
        Destroy(oldestCandle);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateCandle();
        }
    }
}
