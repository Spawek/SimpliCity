using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// Sell offer that doesnt change in time
    /// </summary>
    public class StableSellOffer : SellOffer
    {
        public StableSellOffer(Commodity commodity, int ammount, decimal price, Company seller)
            : base(commodity, ammount, seller)
        {
            initialPrice = price;
        }

        public override decimal PricePerPiece { get { return initialPrice; } }

        private decimal initialPrice;
    }
}
