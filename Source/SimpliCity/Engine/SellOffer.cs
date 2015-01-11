using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SellOffer
    {
        public Commodity Commodity { get; private set; }
        public int Ammount { get; private set; }
        public decimal Price { get; private set; }
        public Company Seller { get; private set; }

        public SellOffer(Commodity _commodity, int _ammount, decimal _price, Company _seller)
        {
            Commodity = _commodity;
            Ammount = _ammount;
            Price = _price;
            Seller = _seller;
        }

        private bool finalized = false;

        public void FinalizeOffer(AssetsOwner buyer)  // TODO: test it!
        {
            if (finalized)
                throw new ApplicationException();
            finalized = true;

            decimal moneyToTransfer = Price * Ammount;
            buyer.TransferMoney(Seller, moneyToTransfer);

            if (!buyer.commodities.ContainsKey(Commodity))
            {
                buyer.commodities.Add(Commodity, Ammount);
            }
            else
            {
                buyer.commodities[Commodity] += Ammount;
            }
        }

        public void FinalizeOfferPartially(AssetsOwner buyer, int partialAmmount)  // TODO: test it!
        {
            if (partialAmmount >= Ammount)
                throw new ApplicationException();

            decimal moneyToTransfer = Price * partialAmmount;
            buyer.TransferMoney(Seller, moneyToTransfer);
            Ammount -= partialAmmount;

            if (!buyer.commodities.ContainsKey(Commodity))
            {
                buyer.commodities.Add(Commodity, 0);
            }

            buyer.commodities[Commodity] += partialAmmount;
        }
    }
}
