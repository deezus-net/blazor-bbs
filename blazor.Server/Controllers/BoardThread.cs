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
    public class BoardThread : Controller
    {
        private readonly DataContext _db;
        public BoardThread(DataContext db)
        {
            _db = db;
        }

        [Route("api/boardthread/")]
        public async Task<List<blazor.Shared.BoardThread>> Get(string q = "")
        {
           return await _db.BoardThreads.OrderByDescending(x => x.LastWritten).ToListAsync();
        }

        [HttpPost]
        [Route("api/boardthread/")]
        public async Task<int> Post([FromBody] blazor.Shared.BoardThread post)
        {
            var now = DateTime.Now;
            post.Id = Guid.NewGuid().ToString();
            post.Created = now;
            post.LastWritten = now;
            post.Count = 1;
            
            var response = post.BoardResponses.First();
            response.Id = Guid.NewGuid().ToString();
            response.BoardThreadId = post.Id;
            response.Created = now;
            
            await _db.BoardThreads.AddAsync(post);
            await _db.BoardResponses.AddAsync(response);
            return await _db.SaveChangesAsync();
        }
    }
}