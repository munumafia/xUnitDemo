using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XUnitDemo.PaymentApi.Models;

namespace XUnitDemo.PaymentApi.Controllers
{
    public class PaymentController : ApiController
    {
        // GET: api/Payment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Payment/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Payment
        public PaymentResponse Post([FromBody]PaymentRequest request)
        {
            return new PaymentResponse
                   {
                       AuthorizationCode = Guid.NewGuid().ToString("N"),
                       PaymentStatus = PaymentStatus.Approved
                   };
        }

        // PUT: api/Payment/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Payment/5
        public void Delete(int id)
        {
        }
    }
}
