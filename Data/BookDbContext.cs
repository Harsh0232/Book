using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBook.Models;

namespace TestBook.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
