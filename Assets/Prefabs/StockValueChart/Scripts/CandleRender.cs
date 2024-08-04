using UnityEngine;
using UnityEngine.UI;

public class CandleRender : MonoBehaviour
{
    public Slider UISlider;
    public Image handle, background;
    public float open, close, max, min;

    public CandleRender(float newOpen, float newClose, float newMax, float newMin)
    {
        open = newOpen;
        close = newClose;
        max = newMax;
        min = newMin;

        UISlider = GetComponent<Slider>();
        handle = transform.Find("Handle").GetComponent<Image>();
        background = transform.Find("Background").GetComponent<Image>();
    }

    void Update()
    {
        SetSliderProperties();
        UpdateCandleWidth();
        ChangeColor();
    }

    void ChangeColor()
    {
        if (close > open)
        {
            background.color = Color.green;
            handle.color = Color.green;
        }
        else
        {
            background.color = Color.red;
            handle.color = Color.red;
        }
    }

    void SetSliderProperties()
    {
        UISlider.value = (Mathf.Abs((open - close) / 2) + Mathf.Min(open, close) - min) / (max - min);
    }

    void UpdateCandleWidth()
    {
        float candleWidth, barWidth, openCloseRatio;

        openCloseRatio = Mathf.Abs(close - open) / max - min;
        barWidth = UISlider.GetComponent<RectTransform>().sizeDelta.x;

        candleWidth = Mathf.Clamp(openCloseRatio * barWidth, 0.0f, barWidth);

        handle.rectTransform.sizeDelta = new Vector2(candleWidth, handle.rectTransform.sizeDelta.y);
    }
}
