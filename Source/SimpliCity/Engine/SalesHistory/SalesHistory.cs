using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface SalesHistory
    {
        void AddTodaySaleData(Commodity commodity, int ammount, decimal pricePerPiece);

        /// <summary></summary>
        /// <param name="commodity"></param>
        /// <param name="date"></param>
        /// <returns>null if there is no data for that commodity</returns>
        decimal? GetAverageSellPrice(Commodity commodity, int date);
    }
}
