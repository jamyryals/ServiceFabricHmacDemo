using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {
        public IHmacRepository Hmacs { get; set; }

        public DemoController()
        {
            Hmacs = new ReliableHmacRepository();
        }

        [HttpPost]
        [Route("transactions")]
        public async Task<HttpResponseMessage> CreateTransaction()
        {
            // Pulling the header should happen in the Request pipeline middleware, showing here for clarity
            var header = Request.Headers.First(x => x.Key.Equals("X-HMAC", StringComparison.InvariantCultureIgnoreCase));
            var hmac = header.Value.First();
            if (await Hmacs.HmacExists(hmac))
            {
                ServiceEventSource.Current.Message($"Hmac: {hmac} exists.");
                return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
            }
            ServiceEventSource.Current.Message($"Hmac: {hmac} doesn't exist.");
            // Create "Transaction"
            return Request.CreateResponse(System.Net.HttpStatusCode.Created);
        }
    }
}
