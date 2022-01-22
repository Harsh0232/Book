using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestBook.Models
{
    public class Book 
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publication { get; set; }
        public DateTime Launched { get; set; }
        public int Price { get; set; }

    }
}
