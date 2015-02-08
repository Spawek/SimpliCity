using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface SellAssistant
    {
        void SellAsset(AssetsOwner seller, Commodity commodity, int ammount);
    }
}
