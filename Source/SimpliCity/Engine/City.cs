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

        public Technology GetTechnology(string name)
        {
            return commonTechnologies.Find(x => x.Name == name);
        }

        /// <summary>
        /// this is more like script setting up the initial state than part of engine
        /// </summary>
        public City()
        {
            commodities.AddRange(CreateCommodities());
            var defaultPrices = CreateDefaultPrices();
            markets.AddRange(CreateMarket(defaultPrices));
            citizens.AddRange(CreateCitizens());
            needs.AddRange(CreateNeeds());
            commonTechnologies.AddRange(CreateTechnologies());
            companies.AddRange(CreateCompanies()); // to be removed (mby?) - companies should be created by citizens, not scripted
        }

        private List<Company> CreateCompanies()
        {
            List<Company> companies = new List<Company>();

            Company biznesJanusza = new Company(
                _name: "Biznes Janusza",
                _shareholders: new Dictionary<AssetsOwner, decimal>() { { GetCitizen("Janusz"), 1M } },
                _city: this,
                _market: GetMarket("Hala Targowa")
            );

            biznesJanusza.commodityStorage.Deposit(GetCommodity("grain"), 20);
            GetCitizen("Janusz").TransferMoney(biznesJanusza, 50.0M);

            var sellAssistantJanusza = new SimpleSellAssistant(GetMarket("Hala Targowa"));
            biznesJanusza.strategy = new SingleProductionStrategy(
                biznesJanusza, GetTechnology("grain plantation"), sellAssistantJanusza);
            companies.Add(biznesJanusza);

            return companies;
        }

        private List<Technology> CreateTechnologies()
        {
            return new List<Technology>()
            {
                new Technology(
                    name: "grain plantation",
                    input: new Dictionary<Commodity, int>() { { GetCommodity("grain"), 10 }, { SpecialCommodities.Work, 1 } },
                    output: new Dictionary<Commodity, int>() { { GetCommodity("grain"), 20 } }),
                new Technology(
                    name: "milk cow",
                    input: new Dictionary<Commodity, int>() { { GetCommodity("cow"), 1 }, { SpecialCommodities.Work, 1 } },
                    output: new Dictionary<Commodity, int>() { { GetCommodity("cow"), 1 }, { GetCommodity("milk"), 10 } }),
                new Technology(
                    name: "reproduce cow",
                    input: new Dictionary<Commodity, int>() { { GetCommodity("cow"), 2 }, { SpecialCommodities.Work, 2 } },
                    output: new Dictionary<Commodity, int>() { { GetCommodity("cow"), 3 } })
            };
        }

        private List<Commodity> CreateCommodities()
        {
            return new List<Commodity>()
            {
                new Commodity(name: "grain", need: GetNeed("hunger")),
                new Commodity(name: "cow", need: null),
                new Commodity(name: "milk", need: GetNeed("hunger")),
                new Commodity(name: "meat", need: GetNeed("hunger"))
            };
        }

        private List<Need> CreateNeeds()
        {
            return new List<Need>()
            {
                new Need(
                    name: "hunger",
                    enjoymentCurveFoo: new Func<int, double>(x => Math.Sqrt(Math.Sqrt(Convert.ToDouble(x)))))
            };
        }

        private List<Market> CreateMarket(IDictionary<Commodity, decimal> defaultPrices)
        {
            return new List<Market>()
            {
                new Market("Hala Targowa", new SimpleSalesHistory(defaultPrices))
            };
        }

        private IDictionary<Commodity, decimal> CreateDefaultPrices()
        {
            return new Dictionary<Commodity, decimal>()
            {
                { SpecialCommodities.Work, 10 },
                { GetCommodity("grain"), 4 },
                { GetCommodity("cow"), 20 },
                { GetCommodity("milk"), 4 },
                { GetCommodity("meat"), 10 },
            };
        }

        private List<Citizen> CreateCitizens()
        {
            return new List<Citizen>()
            {
                new SimpleCitizen(
                    name: "Janusz",
                    money: 100,
                    city: this,
                    sellAssistant: new SimpleSellAssistant(GetMarket("Hala Targowa"))
                ),
                new SimpleCitizen(
                    name: "Grażyna",
                    money: 100,
                    city: this,
                    sellAssistant: new SimpleSellAssistant(GetMarket("Hala Targowa"))
                )
            };
        }
    }
}
