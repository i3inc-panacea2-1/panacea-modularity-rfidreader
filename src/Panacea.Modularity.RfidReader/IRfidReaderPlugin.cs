using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.RfidReader
{
    public interface IRfidReaderPlugin : IPlugin
    {
        event EventHandler<string> CardConnected;
        event EventHandler<string> CardDisconnected;
        void SimulateCardTap(string s);
    }
}
