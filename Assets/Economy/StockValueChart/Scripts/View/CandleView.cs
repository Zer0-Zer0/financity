using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class CandleView : CustomUIComponent
    {
        [SerializeField]
        private Image HighLowShadow;

        [SerializeField]
        private Image OpenClose;

        [SerializeField]
        private RectTransform Candle;

        public void SetCandleCoords(candleData data){
        }


        protected override void Setup() { }

        protected override void Configure() { }
    }
}
