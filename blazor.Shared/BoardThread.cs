using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazor.Shared
{
    public class BoardThread
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }

        public int Count { get; set; }
        public DateTime LastWritten { get; set; }
        
        public ICollection<BoardResponse> BoardResponses { get; set; }

       
    }
}