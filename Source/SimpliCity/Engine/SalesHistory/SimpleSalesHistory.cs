using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SimpleSalesHistory : SalesHistory
    {
        public SimpleSalesHistory(IDictionary<Commodity, decimal> defaultPrices)
        {
            foreach (var item in defaultPrices)
            {
                actualPrices.Add(item.Key, new DateLowHigh(0, item.Value, item.Value));
            }
        }

        public void AddTodaySaleData(Commodity commodity, int ammount, decimal pricePerPiece)
        {
            AddSellData(commodity, ammount, pricePerPiece, TurnCounter.Now);
        }

        private void AddSellData(Commodity commodity, int ammount, decimal pricePerPiece, int date)
        {
            AddSellDataToHistoricData(commodity, ammount, pricePerPiece, date);
            AddSellDataToActualPrices(commodity, pricePerPiece, date);
        }

        private void AddSellDataToActualPrices(Commodity commodity, decimal pricePerPiece, int date)
        {
            if (!actualPrices.ContainsKey(commodity))
                throw new ApplicationException();

            actualPrices[commodity].Update(date, pricePerPiece);
        }

        private void AddSellDataToHistoricData(Commodity commodity, int ammount, decimal pricePerPiece, int date)
        {
            if (!historicData.ContainsKey(date))
                historicData.Add(date, new Dictionary<Commodity, AmmountPrice>());

            if (!historicData[date].ContainsKey(commodity))
            {
                historicData[date].Add(commodity, new AmmountPrice(ammount, pricePerPiece * ammount));
            }
            else
            {
                var ammountPrice = historicData[date][commodity];
                ammountPrice.ammount += ammount;
                ammountPrice.totalPrice += pricePerPiece * ammount;
            }
        }

        public decimal? GetAverageSellPrice(Commodity commodity, int date)
        {
            if (!historicData.ContainsKey(date))
                return null;
            if (!historicData[date].ContainsKey(commodity))
                return null;

            var ammountPrice = historicData[date][commodity];
            return (decimal)ammountPrice.totalPrice / ammountPrice.ammount;
        }

        public decimal GetActualPrice(Commodity commodity)
        {
            var commodityPrice = actualPrices[commodity];
            return (commodityPrice.Low + commodityPrice.High) / 2;
        }

        private class AmmountPrice
        {
            public AmmountPrice(int ammount, decimal totalPrice)
            {
                this.ammount = ammount;
                this.totalPrice = totalPrice;
            }

            public int ammount;
            public decimal totalPrice;
        }

        private class DateLowHigh
        {
            public DateLowHigh(int newestTradeDate, decimal low, decimal high)
            {
                NewestTradeDate = newestTradeDate;
                Low = low;
                High = high;
            }

            /// <summary>
            /// updates object using new trade date (its date and price)
            /// </summary>
            /// <param name="newestTradeDate"></param>
            /// <param name="pricePerPiece"></param>
            public void Update(int newestTradeDate, decimal pricePerPiece)
            {
                if (NewestTradeDate < newestTradeDate)
                {
                    Low = pricePerPiece;
                    High = pricePerPiece;
                    NewestTradeDate = newestTradeDate;
                }
                if (NewestTradeDate == newestTradeDate)
                {
                    Low = Math.Min(Low, pricePerPiece);
                    High = Math.Max(High, pricePerPiece);
                }
                else
                {
                    throw new ApplicationException();
                }
            }

            public int NewestTradeDate { get; private set; }
            public decimal Low { get; private set; }
            public decimal High { get; private set; }
        }

        private IDictionary<int, IDictionary<Commodity, AmmountPrice>> historicData = new Dictionary<int, IDictionary<Commodity, AmmountPrice>>();
        private IDictionary<Commodity, DateLowHigh> actualPrices = new Dictionary<Commodity, DateLowHigh>();
    }
}
