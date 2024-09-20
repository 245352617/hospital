using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiJian.CardReader.CardReaders.DeCard;
using YiJian.CardReader.CardReaders.Donsee;
using YiJian.CardReader.CardReaders.Wante;

namespace YiJian.CardReader.CardReaders
{
    public static class CardReaderFactory
    {
        public static ICardReader? Create(string vendor)
        {
            if (Vendors.Wante.Name == vendor)
            {
                return new WanteReader(Vendors.Wante.DriverPath);
            }

            if (Vendors.DeCard.Name == vendor)
            {
                return new DeCardReader(Vendors.DeCard.DriverPath);
            }

            if (Vendors.Donsee.Name == vendor)
            {
                return new DonseeReader(Vendors.Donsee.DriverPath);
            }

            return null;
        }
    }
}
