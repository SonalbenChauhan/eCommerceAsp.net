using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace eCommerceASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public ValuesController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.ObjectsToSend.Count()==0)
            {
                context.ObjectsToSend.AddRange(new List<ObjectToSend>
                {
                  new ObjectToSend{Data1 = "value1", Data2="value2"},
                new ObjectToSend{Data1 = "value3", Data2="value4"},


                });
                context.SaveChanges();

            }
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<List<ObjectToSend>> Get()
        {
            return context.ObjectsToSend.ToList();
        }

       


        // GET api/values/5

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectToSend>> Get(int id) => await context.ObjectsToSend.SingleOrDefaultAsync(o => o.Id == id);

        // POST api/values
        [HttpPost]
        //public void Post([FromBody] string value)
        public async Task<PostResponse> Post([FromBody]ObjectToSend objectToSend)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.ObjectsToSend.AddAsync(objectToSend);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = objectToSend.Id;

            }
            catch { }
            return pr;
        }
      

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
