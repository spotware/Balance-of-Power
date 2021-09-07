using cAlgo.API;
using cAlgo.API.Indicators;

namespace cAlgo
{
    [Indicator(IsOverlay = false, TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class BalanceofPower : Indicator
    {
        private IndicatorDataSeries _bop;
        private MovingAverage _ma;

        [Parameter("Periods", DefaultValue = 14)]
        public int Periods { get; set; }

        [Parameter("MA Type", DefaultValue = MovingAverageType.Simple)]
        public MovingAverageType MaType { get; set; }

        [Output("Main")]
        public IndicatorDataSeries Result { get; set; }

        protected override void Initialize()
        {
            _bop = CreateDataSeries();
            _ma = Indicators.MovingAverage(_bop, Periods, MaType);
        }

        public override void Calculate(int index)
        {
            var bar = Bars[index];

            _bop[index] = (bar.Close - bar.Open) / (bar.High - bar.Low);
            Result[index] = _ma.Result[index];
        }
    }
}