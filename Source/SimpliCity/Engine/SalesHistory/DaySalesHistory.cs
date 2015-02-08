using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class DaySalesHistory : SalesHistory
    {
        public void AddTodaySaleData(Commodity commodity, int ammount, decimal pricePerPiece)
        {
            AddSellData(commodity, ammount, pricePerPiece, TurnCounter.Now);
        }

        private void AddSellData(Commodity commodity, int ammount, decimal pricePerPiece, int date)
        {
            if (!data.ContainsKey(date))
                data.Add(date, new Dictionary<Commodity, AmmountPrice>());

            if (!data[date].ContainsKey(commodity))
            {
                data[date].Add(commodity, new AmmountPrice(ammount, pricePerPiece));
            }
            else
            {
                var ammountPrice = data[date][commodity];
                ammountPrice.ammount += ammount;
                ammountPrice.totalPrice += pricePerPiece * ammount;
            }
        }

        public decimal? GetAverageSellPrice(Commodity commodity, int date)
        {
            if (!data.ContainsKey(date))
                return null;
            if (!data[date].ContainsKey(commodity))
                return null;

            var ammountPrice = data[date][commodity];
            return (decimal)ammountPrice.totalPrice / ammountPrice.ammount;
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

        private IDictionary<int, IDictionary<Commodity, AmmountPrice>> data = new Dictionary<int, IDictionary<Commodity, AmmountPrice>>();
    }
}
