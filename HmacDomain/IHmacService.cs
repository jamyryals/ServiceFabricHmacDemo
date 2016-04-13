namespace HmacDomain
{
    using System.Threading.Tasks;
    using Microsoft.ServiceFabric.Services.Remoting;

    public interface IHmacService : IService
    {
        Task<bool> HmacExists(string hmac);
    }
}
