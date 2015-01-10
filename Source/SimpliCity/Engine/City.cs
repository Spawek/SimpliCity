﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class City
    {
        public List<Citizen> citizens = new List<Citizen>();
        public List<Company> companies = new List<Company>();
        public List<Technology> commonTechnologies = new List<Technology>();
        public List<Market> markets = new List<Market>();
        public List<Need> needs = new List<Need>(); // should be more global
        public List<Commodity> commodities = new List<Commodity>(); // should be more global

        public Citizen GetCitizen(string name)
        {
            return citizens.Find(x => x.name == name);
        }

        public Market GetMarket(string name)
        {
            return markets.Find(x => x.Name == name);
        }

        public Need GetNeed(string name)
        {
            return needs.Find(x => x.name == name);
        }

        public Commodity GetCommodity(string name)
        {
            return commodities.Find(x => x.name == name);
        }

        public City()
        {
            Market halaTargowa = new Market("Hala Targowa");
            markets.Add(halaTargowa);

            citizens.Add(new SimpleCitizen(
                name: "Janusz",
                money: 50,
                city: this
            ));

            citizens.Add(new SimpleCitizen(
                name: "Grażyna",
                money: 100,
                city: this
            ));

            Need hunger = new Need()
            {
                name = "hunger",
                enjoymentCurveFoo = new Func<int,double>(x => Math.Sqrt(Math.Sqrt(Convert.ToDouble(x))))
            };
            needs.Add(hunger);

            Commodity grain = new Commodity()
            {
                name = "grain",
                need = GetNeed("hunger")
            };
            commodities.Add(grain);

            Technology grainPlantation = new Technology()
            {
                name = "grain plantation",
                labourNeeded = 1,
                input = new Dictionary<Commodity, int>() { { GetCommodity("grain"), 10 } },
                output = new Dictionary<Commodity, int>() { { GetCommodity("grain"), 20 } }
            };
            commonTechnologies.Add(grainPlantation);

            Company biznesJanusza = new Company()
            {
                name = "Biznes Janusza",
                money = 50,
                shareholders = new Dictionary<AssetsOwner, decimal>() { { GetCitizen("Janusz"), 1 } },
                city = this,
                market = GetMarket("Hala Targowa"),
                commodities = new Dictionary<Commodity, int>() { { grain, 20 } }
            };
            biznesJanusza.Hire(GetCitizen("Grażyna"), 10);
            biznesJanusza.strategy = new SingleProductionStrategy(biznesJanusza, grainPlantation);
            companies.Add(biznesJanusza);
        }
    }
}