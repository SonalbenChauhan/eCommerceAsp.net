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
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public ItemsController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.Items.Count() == 0)
            {
                context.Items.AddRange(new List<Items>
                {
               //   new Addresses{Data1 = "value1", Data2="value2"},
               // new Addresses{Data1 = "value3", Data2="value4"},

                     new Items{},
                });
                context.SaveChanges();

            }
        }
        // GET: api/Items
        [HttpGet]
        public ActionResult<List<Items>> Get()
        {
            return context.Items.ToList();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> Get(int id) => await context.Items.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/Items
        [HttpPost]
        public async Task<PostResponse> Post([FromBody]Items items)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Items.AddAsync(items);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = items.Id;

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
        public async Task<PutResponse> Put(int id, [FromBody] Items items)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (items.Id > 0 && items.Name.Length > 0 && items.Price> 0)
                {
                    var oldItems = await context.Items.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldItems != null)
                    {
                        
                        oldItems.Name = items.Name;
                        oldItems.Price = items.Price;
                        
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
