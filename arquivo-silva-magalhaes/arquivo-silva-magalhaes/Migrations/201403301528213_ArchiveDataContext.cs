namespace ArquivoSilvaMagalhaes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchiveDataContext : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuthorTexts", "Id", "dbo.Authors");
            DropForeignKey("dbo.Documents", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Documents", "CollectionId", "dbo.Collections");
            DropIndex("dbo.AuthorTexts", new[] { "Id" });
            DropIndex("dbo.Documents", new[] { "CollectionId" });
            DropIndex("dbo.Documents", new[] { "AuthorId" });
            RenameColumn(table: "dbo.AuthorTexts", name: "Id", newName: "Author_Id");
            RenameColumn(table: "dbo.Documents", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Documents", name: "CollectionId", newName: "Collection_Id");
            DropPrimaryKey("dbo.AuthorTexts");
            AlterColumn("dbo.AuthorTexts", "Author_Id", c => c.Int());
            AlterColumn("dbo.Documents", "ResponsibleName", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "DocumentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Documents", "Collection_Id", c => c.Int());
            AlterColumn("dbo.Documents", "Author_Id", c => c.Int());
            AddPrimaryKey("dbo.AuthorTexts", "LanguageCode");
            CreateIndex("dbo.AuthorTexts", "Author_Id");
            CreateIndex("dbo.Documents", "Author_Id");
            CreateIndex("dbo.Documents", "Collection_Id");
            AddForeignKey("dbo.AuthorTexts", "Author_Id", "dbo.Authors", "Id");
            AddForeignKey("dbo.Documents", "Author_Id", "dbo.Authors", "Id");
            AddForeignKey("dbo.Documents", "Collection_Id", "dbo.Collections", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "Collection_Id", "dbo.Collections");
            DropForeignKey("dbo.Documents", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.AuthorTexts", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Documents", new[] { "Collection_Id" });
            DropIndex("dbo.Documents", new[] { "Author_Id" });
            DropIndex("dbo.AuthorTexts", new[] { "Author_Id" });
            DropPrimaryKey("dbo.AuthorTexts");
            AlterColumn("dbo.Documents", "Author_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "Collection_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Documents", "DocumentDate", c => c.String());
            AlterColumn("dbo.Documents", "ResponsibleName", c => c.String());
            AlterColumn("dbo.AuthorTexts", "Author_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AuthorTexts", new[] { "Id", "LanguageCode" });
            RenameColumn(table: "dbo.Documents", name: "Collection_Id", newName: "CollectionId");
            RenameColumn(table: "dbo.Documents", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.AuthorTexts", name: "Author_Id", newName: "Id");
            CreateIndex("dbo.Documents", "AuthorId");
            CreateIndex("dbo.Documents", "CollectionId");
            CreateIndex("dbo.AuthorTexts", "Id");
            AddForeignKey("dbo.Documents", "CollectionId", "dbo.Collections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Documents", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AuthorTexts", "Id", "dbo.Authors", "Id", cascadeDelete: true);
        }
    }
}
