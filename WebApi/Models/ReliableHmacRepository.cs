using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using HmacDomain;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using WebApi.Models;

namespace WebApi
{
    public class ReliableHmacRepository : IHmacRepository
    {
        private long defaultPartitionID = 1;
        private Uri hmacServiceInstance = new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/HmacService");

        public Task<bool> HmacExists(string hmac)
        {
            try
            {
                IHmacService proxy = ServiceProxy.Create<IHmacService>(this.defaultPartitionID, hmacServiceInstance);
                return proxy.HmacExists(hmac);
            }
            catch (Exception ex)
            {
                ServiceEventSource.Current.Message($"Failed to check HMAC: {ex.ToString()}");
                throw;
            }
        }
    }

    public class InMemoryHmacRepository : IHmacRepository
    {
        List<string> repo = new List<string>();

        public async Task<bool> HmacExists(string hmac)
        {
            if (!repo.Contains(hmac))
            {
                repo.Add(hmac);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SharedCacheHmacRepository : IHmacRepository
    {
        public Task<bool> HmacExists(string hmac)
        {
            throw new NotImplementedException();
        }
    }
}