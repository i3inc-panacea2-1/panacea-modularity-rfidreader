using Panacea.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modularity.RfidReader
{
    public class RfidReaderPluginContainer
    {
        private readonly IPluginLoader _loader;

        internal RfidReaderPluginContainer(IPluginLoader loader)
        {
            _loader = loader;
            _loader.PluginLoaded += _loader_PluginLoaded;
            _loader.PluginUnloaded += _loader_PluginUnloaded;
            AllReaders.ToList().ForEach(r =>
            {
                r.CardConnected += Rfid_CardConnected;
                r.CardDisconnected += Rfid_CardDisconnected;
            });
        }

        private void _loader_PluginUnloaded(object sender, IPlugin e)
        {
            var rfid = e as IRfidReaderPlugin;
            if (rfid == null) return;
            rfid.CardConnected -= Rfid_CardConnected;
            rfid.CardDisconnected -= Rfid_CardDisconnected;
        }

        private void _loader_PluginLoaded(object sender, IPlugin e)
        {
            var rfid = e as IRfidReaderPlugin;
            if (rfid == null) return;
            rfid.CardConnected += Rfid_CardConnected;
            rfid.CardDisconnected += Rfid_CardDisconnected;
        }

        private void Rfid_CardDisconnected(object sender, string e)
        {
            CardDisconnected?.Invoke(sender, e);
        }

        private void Rfid_CardConnected(object sender, string e)
        {
            CardConnected?.Invoke(sender, e);
        }

        IReadOnlyList<IRfidReaderPlugin> AllReaders {
            get => _loader
                .GetPlugins<IRfidReaderPlugin>()
                .ToList()
                .AsReadOnly();
        }

        event EventHandler<string> CardConnected;

        event EventHandler<string> CardDisconnected;

        void SimulateCardTap(string s)
        {
            var i = new Random().Next(0, AllReaders.Count);
            AllReaders[i].SimulateCardTap(s);
        }
    }
}
