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
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public OrderItemsController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.Orderitems.Count() == 0)
            {
                context.Orderitems.AddRange(new List<Orderitems>
                {
               //   new Addresses{Data1 = "value1", Data2="value2"},
               // new Addresses{Data1 = "value3", Data2="value4"},

                     new Orderitems{},
                });
                context.SaveChanges();

            }
        }
        // GET: api/OrderItems
        [HttpGet]
        public ActionResult<List<Orderitems>> Get()
        {
            return context.Orderitems.ToList();
        }
        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orderitems>> Get(int id) => await context.Orderitems.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/OrderItems
        [HttpPost]
        //public void Post([FromBody] string value)
        public async Task<PostResponse> Post([FromBody]Orderitems orderItems)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Orderitems.AddAsync(orderItems);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = orderItems.Id;

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
        public async Task<PutResponse> Put(int id, [FromBody] Orderitems orderitems)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (orderitems.Id > 0 && orderitems.ItemId > 0 && orderitems.OrderId > 0 && orderitems.Quantity > 0)
                {
                    var oldOrderItems = await context.Orderitems.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldOrderItems != null)
                    {

                        oldOrderItems.ItemId = orderitems.ItemId;

                        oldOrderItems.OrderId = orderitems.OrderId;

                        oldOrderItems.Quantity = orderitems.Quantity;


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
