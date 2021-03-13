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
    public class OrderController : ControllerBase
    {
      
            private readonly AppDbContext context;

            public class PostResponse
            {
                public bool success { get; set; }
                public int Id { get; set; }

            }
            public OrderController(AppDbContext appDbContext)
            {
                context = appDbContext;
                if (context.Order.Count() == 0)
                {
                    context.Order.AddRange(new List<Order>
                {
               //   new Addresses{Data1 = "value1", Data2="value2"},
               // new Addresses{Data1 = "value3", Data2="value4"},

                     new Order{},
                });
                    context.SaveChanges();

                }
            }
            // GET: api/Order
            [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            return context.Order.ToList();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id) => await context.Order.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/Order
        [HttpPost]
        public async Task<PostResponse> Post([FromBody]Order order)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Order.AddAsync(order);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = order.Id;

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
        public async Task<PutResponse> Put(int id, [FromBody] Order order)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (order.Id > 0 && order.UserId > 0 && order.GrandTotal > 0)
                {
                    var oldOrder = await context.Order.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldOrder != null)
                    {

                       
                        oldOrder.UserId = order.UserId;
                        oldOrder.GrandTotal = order.GrandTotal;


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
