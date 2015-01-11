using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    abstract public class AssetsOwner
    {
        public AssetsOwner(decimal initialMoney)
        {
            money = initialMoney;
        }

        public decimal money { get; private set; }
        public IDictionary<Company, decimal> companyShares = new Dictionary<Company, decimal>();
        public IDictionary<Commodity, int> commodities = new Dictionary<Commodity, int>();

        public void TransferMoney(AssetsOwner other, decimal ammount)
        {
            if (this.money < ammount)
                throw new ApplicationException();

            this.money -= ammount;
            other.money += ammount;
        }
    }
}
