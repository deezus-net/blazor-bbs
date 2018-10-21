using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blazor.Shared
{
    public class BoardResponse
    {
        [Key]
        public string Id { get; set; }
        public string BoardThreadId { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        
        [NotMapped]
        [ForeignKey("BoardThreadId")]
        public BoardThread BoardThread { get; set; }
    }
}