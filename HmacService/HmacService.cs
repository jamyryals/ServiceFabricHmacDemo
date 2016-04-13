using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HmacDomain;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Data;

namespace HmacService
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance.
    /// </summary>
    internal sealed class HmacService : StatefulService, IHmacService
    {
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new List<ServiceReplicaListener> {
                new ServiceReplicaListener(initParams => new ServiceRemotingListener<IHmacService>(initParams, this))
            };
        }

        protected override Task RunAsync(CancellationToken cancellationToken)
        {
            return base.RunAsync(cancellationToken);
        }

        public async Task<bool> HmacExists(string hmac)
        {
            IReliableDictionary<string, DateTime> hmacDictionary = 
                await this.StateManager.GetOrAddAsync<IReliableDictionary<string, DateTime>>("hmac");
            bool exists = true;
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                if (!await hmacDictionary.ContainsKeyAsync(tx, hmac))
                {
                    await hmacDictionary.AddAsync(tx, hmac, DateTime.UtcNow);
                    exists = false;
                }
                else
                {
                    exists = true;
                }
                await tx.CommitAsync();
            }
            return exists;
        }
    }
}
