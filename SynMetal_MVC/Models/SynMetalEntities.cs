using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SynMetal_MVC.Models
{
    class SynMetalEntities : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProdCategories { get; set; }
        public DbSet<PhotoFile> Photos { get; set; }
        public DbSet<PdfFile> PdfFiles { get; set; }
        public DbSet<PdfCategory> PdfCategories { get; set; }
        public DbSet<NewsModel> News { get; set; }
        public SynMetalEntities():base("DefaultConnection")
        { }
    }
}
