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
    public class CommentsController : ControllerBase
    {

        private readonly AppDbContext context;

        public class PostResponse
        {
            public bool success { get; set; }
            public int Id { get; set; }

        }
        public CommentsController(AppDbContext appDbContext)
        {
            context = appDbContext;
            if (context.Comments.Count() == 0)
            {
                context.Comments.AddRange(new List<Comments>
                {
               //   new Addresses{Data1 = "value1", Data2="value2"},
               // new Addresses{Data1 = "value3", Data2="value4"},

                     new Comments{},
                });
                context.SaveChanges();

            }
        }
        // GET: api/Comments
        [HttpGet]
        public ActionResult<List<Comments>> Get()
        {
            return context.Comments.ToList();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> Get(int id) => await context.Comments.SingleOrDefaultAsync(o => o.Id == id);


        // POST: api/Comments
        [HttpPost]
        public async Task<PostResponse> Post([FromBody]Comments comments)

        {
            var pr = new PostResponse()
            {
                success = false,
                Id = 0
            };
            try
            {
                await context.Comments.AddAsync(comments);
                await context.SaveChangesAsync();
                pr.success = true;
                pr.Id = comments.Id;

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
        public async Task<PutResponse> Put(int id, [FromBody] Comments comments)
        {
            var pr = new PutResponse() { Success = false };

            try
            {
                if (comments.Id > 0 && comments.ItemId > 0 && comments.UserId > 0 && comments.CommentBody.Length > 0 && comments.CommentTitle.Length > 0 && comments.Rating > 0)
                {
                    var oldComments = await context.Comments.SingleOrDefaultAsync(s => s.Id == id);
                    if (oldComments != null)
                    {
                        
                        oldComments.ItemId = comments.ItemId;
                        oldComments.UserId = comments.UserId;
                        oldComments.CommentBody = comments.CommentBody;
                        oldComments.CommentTitle = comments.CommentTitle;
                        oldComments.Rating = comments.Rating;
                        

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
