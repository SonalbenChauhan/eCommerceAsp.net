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
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public UsersController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.Users.Count() == 0)
            {
                context.Users.AddRange(new List<Users>
                {
                  //new ObjectToSend{Data1 = "value1", Data2="value2"},
                //new ObjectToSend{Data1 = "value3", Data2="value4"},

                    new Users{},
                });
                context.SaveChanges();

            }
        }
        // GET: api/Users
        [HttpGet]
        public ActionResult<List<Users>> Get()
        {
            return context.Users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id) => await context.Users.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/Users
        [HttpPost]
        //public void Post([FromBody] string value)
        public async Task<PostResponse> Post([FromBody]Users users)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Users.AddAsync(users);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = users.Id;

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
        public async Task<PutResponse> Put(int id, [FromBody] Users users)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (users.Id >  0 && users.Email.Length > 0 && users.FirstName.Length > 0 && users.LastName.Length > 0 && users.Password.Length > 0)
                {
                    var oldUser = await context.Users.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldUser != null)
                    {
                        oldUser.Email = users.Email;
                        oldUser.FirstName = users.FirstName;
                        oldUser.LastName = users.LastName;
                        oldUser.Password = oldUser.Password;
                     

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
