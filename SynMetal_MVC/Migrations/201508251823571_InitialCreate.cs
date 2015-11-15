namespace SynMetal_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsModels",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId);
            
            CreateTable(
                "dbo.PdfCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.PdfFiles",
                c => new
                    {
                        PdfId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        FileName = c.String(),
                        Description = c.String(),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PdfId)
                .ForeignKey("dbo.PdfCategories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.PhotoFiles",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        Category_CategoryId = c.Int(nullable: false),
                        Photo_PhotoId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PhotoFiles", t => t.Photo_PhotoId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Photo_PhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Photo_PhotoId", "dbo.PhotoFiles");
            DropForeignKey("dbo.Products", "Category_CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.PdfFiles", "Category_CategoryId", "dbo.PdfCategories");
            DropIndex("dbo.Products", new[] { "Photo_PhotoId" });
            DropIndex("dbo.Products", new[] { "Category_CategoryId" });
            DropIndex("dbo.PdfFiles", new[] { "Category_CategoryId" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.PhotoFiles");
            DropTable("dbo.PdfFiles");
            DropTable("dbo.PdfCategories");
            DropTable("dbo.NewsModels");
        }
    }
}
