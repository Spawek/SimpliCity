using System;
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
            return citizens.Find(x => x.Name == name);
        }

        public Market GetMarket(string name)
        {
            return markets.Find(x => x.Name == name);
        }

        public Need GetNeed(string name)
        {
            return needs.Find(x => x.Name == name);
        }

        public Commodity GetCommodity(string name)
        {
            return commodities.Find(x => x.Name == name);
        }

        public City()
        {
            Market halaTargowa = new Market("Hala Targowa", new DaySalesHistory());
            markets.Add(halaTargowa);

            citizens.Add(new SimpleCitizen(
                name: "Janusz",
                money: 100,
                city: this,
                sellAssistant: new SimpleSellAssistant(halaTargowa)
            ));

            citizens.Add(new SimpleCitizen(
                name: "Grażyna",
                money: 100,
                city: this,
                sellAssistant: new SimpleSellAssistant(halaTargowa)
            ));

            Need hunger = new Need(
                name: "hunger",
                enjoymentCurveFoo: new Func<int, double>(x => Math.Sqrt(Math.Sqrt(Convert.ToDouble(x))))
            );
            needs.Add(hunger);

            Commodity grain = new Commodity(
                name: "grain",
                need: GetNeed("hunger")
            );
            commodities.Add(grain);

            Technology grainPlantation = new Technology(
                name: "grain plantation",
                labourNeeded: 1,
                input: new Dictionary<Commodity, int>() { { GetCommodity("grain"), 10 }, { SpecialCommodities.Work, 1 } },
                output: new Dictionary<Commodity, int>() { { GetCommodity("grain"), 20 } }
            );
            commonTechnologies.Add(grainPlantation);

            Company biznesJanusza = new Company(
                _name: "Biznes Janusza",
                _shareholders: new Dictionary<AssetsOwner, decimal>() { { GetCitizen("Janusz"), 1M } },
                _city: this,
                _market: GetMarket("Hala Targowa")
            );
            biznesJanusza.commodities = new Dictionary<Commodity, int>() { { grain, 20 } };
            GetCitizen("Janusz").TransferMoney(biznesJanusza, 50.0M);

            var sellAssistantJanusza = new SimpleSellAssistant(halaTargowa);
            biznesJanusza.strategy = new SingleProductionStrategy(biznesJanusza, grainPlantation, sellAssistantJanusza);
            companies.Add(biznesJanusza);
        }
    }
}
