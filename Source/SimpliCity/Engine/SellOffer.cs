﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class SellOffer
    {
        public Commodity commodity;
        public int ammount;
        public decimal price;
        public Company seller;

        public SellOffer(Commodity _commodity, int _ammount, decimal _price, Company _seller)
        {
            commodity = _commodity;
            ammount = _ammount;
            price = _price;
            seller = _seller;
        }

        private bool finalized = false;

        public void FinalizeOffer(AssetsOwner buyer)  // TODO: test it!
        {
            if (finalized)
                throw new ApplicationException();
            finalized = true;

            decimal moneyToTransfer = price * ammount;
            buyer.TransferMoney(seller, moneyToTransfer);

            if (!buyer.commodities.ContainsKey(commodity))
            {
                buyer.commodities.Add(commodity, ammount);
            }
            else
            {
                buyer.commodities[commodity] += ammount;
            }
        }

        public void FinalizeOfferPartially(AssetsOwner buyer, int partialAmmount)  // TODO: test it!
        {
            if (partialAmmount >= ammount)
                throw new ApplicationException();

            decimal moneyToTransfer = price * partialAmmount;
            buyer.TransferMoney(seller, moneyToTransfer);
            ammount -= partialAmmount;

            if (!buyer.commodities.ContainsKey(commodity))
            {
                buyer.commodities.Add(commodity, 0);
            }

            buyer.commodities[commodity] += partialAmmount;
        }
    }
}
