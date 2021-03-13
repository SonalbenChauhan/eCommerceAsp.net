using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public AddressesController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.Addresses.Count() == 0)
            {
                context.Addresses.AddRange(new List<Addresses>
                {
               //   new Addresses{Data1 = "value1", Data2="value2"},
               // new Addresses{Data1 = "value3", Data2="value4"},

                     new Addresses{},
                });
                context.SaveChanges();

            }
        }
        // GET: api/Addresses
        [HttpGet]
        public ActionResult<List<Addresses>> Get()
        {
            return context.Addresses.ToList();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addresses>> Get(int id) => await context.Addresses.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/Addresses
        [HttpPost]
       
        //public void Post([FromBody] string value)
        public async Task<PostResponse> Post([FromBody]Addresses addresses)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Addresses.AddAsync(addresses);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = addresses.Id;

            }
            catch { }
            return pr;
        }

        public class PutResponse
        {
            public bool Success { get; set; }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<PutResponse> Put(int id, [FromBody] Addresses addresses)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (addresses.Id > 0 && addresses.UserId > 0 && addresses.City.Length > 0 && addresses.Country.Length > 0 && addresses.Line1.Length> 0 && addresses.Line2.Length > 0 && addresses.Phone > 0 && addresses.PostalCode.Length > 0)
                {
                    var oldAddress = await context.Addresses.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldAddress != null)
                    {
                        oldAddress.UserId = addresses.UserId;
                        oldAddress.City = addresses.City;
                        oldAddress.Country = addresses.Country;
                        oldAddress.Line1 = addresses.Line1;
                        oldAddress.Line2 = addresses.Line2;
                        oldAddress.Phone = addresses.Phone;
                        oldAddress.PostalCode = addresses.PostalCode;

                        await context.SaveChangesAsync();
                        pr.Success = true;
                    }
                }
            }
            catch { }

            return pr;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
