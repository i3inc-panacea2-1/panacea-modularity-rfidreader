using Panacea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.RfidReader
{
    public static class PanaceaServicesExtensions
    {
        private static readonly object padlock = new object();
        private static volatile RfidReaderPluginContainer _instance;

        public static RfidReaderPluginContainer GetRfidReaderContainer(this PanaceaServices services)
        {
            if(_instance == null)
            {
                lock (padlock)
                {
                    if(_instance == null)
                    {
                        _instance = new RfidReaderPluginContainer(services.PluginLoader);
                    }
                }
            }
            return _instance;
        }
    }
}
