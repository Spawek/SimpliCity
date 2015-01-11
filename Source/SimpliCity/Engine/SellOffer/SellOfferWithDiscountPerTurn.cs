using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SellOfferWithDiscountPerTurn : SellOffer
    {
        public SellOfferWithDiscountPerTurn(Commodity commodity, int ammount,
            decimal price, Company seller, double discount)
            : base(commodity, ammount, seller)
        {
            if (discount <= 0.0d || discount >= 1.0d)
                throw new ArgumentException();

            this.discount = discount;
            this.initialPrice = price;
            offerBegginingTurn = TurnCounter.Now;
        }

        public override decimal Price
        {
            get
            {
                int turnsPassed = TurnCounter.Now - offerBegginingTurn;
                double discountFactor = Math.Pow(1.0d - discount, turnsPassed);
                decimal outputPrice = (decimal)((double)initialPrice * discountFactor);

                return outputPrice;
            }
        }

        private double discount;
        private decimal initialPrice;
        private int offerBegginingTurn;
    }
}
