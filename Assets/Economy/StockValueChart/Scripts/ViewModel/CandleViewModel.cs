using UnityEngine;

namespace UISystem
{
    public class CandleViewModel : MonoBehaviour
    {
        /*git
        public void SetCandle(candleData data)
        {
            float percentualHigh = CalculateCandlePositionRatio(data, data.highShadow);
            float percentualLow = CalculateCandlePositionRatio(data, data.lowShadow);
            float percentualOpen = CalculateCandlePositionRatio(data, data.openValue);
            float percentualClose = CalculateCandlePositionRatio(data, data.closeValue);
        }

        private float CalculateCandlePositionRatio(candleData data, float x)
        {
            float divisor = data.HistoricalMaximum - data.HistoricalMinimum;
            float denominator = x - data.HistoricalMinimum;
            float result = denominator / divisor;
            return result;
        }*/
    }
}
