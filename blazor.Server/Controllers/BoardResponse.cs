using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blazor.Server.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace blazor.Server.Controllers
{
    public class BoardResponse : Controller
    {
        private readonly DataContext _db;
        public BoardResponse(DataContext db)
        {
            _db = db;
        }

        [Route("api/boardresponse/{threadId}")]
        public async Task<blazor.Shared.BoardThread> Get(string threadId, string q = "")
        {
            return await _db.BoardThreads.Include(x => x.BoardResponses).FirstOrDefaultAsync(x => x.Id == threadId);
        }

        [HttpPost]
        [Route("api/boardresponse/{threadId}")]
        public async Task Post(string threadId, [FromBody] blazor.Shared.BoardResponse post)
        {
            var now = DateTime.Now;
            post.Id = Guid.NewGuid().ToString();
            post.BoardThreadId = threadId;
            post.Created = now;
            await _db.BoardResponses.AddAsync(post);
            await _db.SaveChangesAsync();

            var thread = await _db.BoardThreads.FindAsync(threadId);
            thread.LastWritten = now;
            thread.Count = _db.BoardResponses.Count(x => x.BoardThreadId == threadId);
            await _db.SaveChangesAsync();
        }
    }
}