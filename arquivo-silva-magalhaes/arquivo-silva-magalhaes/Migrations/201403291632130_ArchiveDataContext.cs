namespace ArquivoSilvaMagalhaes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchiveDataContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Authors", "DeathDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Authors", "DeathDate", c => c.String());
            AlterColumn("dbo.Authors", "BirthDate", c => c.String());
        }
    }
}
