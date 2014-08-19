namespace ArquivoSilvaMagalhaes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchiveDataContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Keywords", "Image_Id", "dbo.Images");
            DropIndex("dbo.Keywords", new[] { "Image_Id" });
            CreateTable(
                "dbo.KeywordImages",
                c => new
                    {
                        Keyword_Id = c.Int(nullable: false),
                        Image_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Keyword_Id, t.Image_Id })
                .ForeignKey("dbo.Keywords", t => t.Keyword_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.Keyword_Id)
                .Index(t => t.Image_Id);
            
            DropColumn("dbo.Keywords", "Image_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Keywords", "Image_Id", c => c.Int());
            DropForeignKey("dbo.KeywordImages", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.KeywordImages", "Keyword_Id", "dbo.Keywords");
            DropIndex("dbo.KeywordImages", new[] { "Image_Id" });
            DropIndex("dbo.KeywordImages", new[] { "Keyword_Id" });
            DropTable("dbo.KeywordImages");
            CreateIndex("dbo.Keywords", "Image_Id");
            AddForeignKey("dbo.Keywords", "Image_Id", "dbo.Images", "Id");
        }
    }
}
