namespace Werewolf_Beta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "NemesisID" });
            AlterColumn("dbo.Users", "NemesisID", c => c.Int());
            CreateIndex("dbo.Users", "NemesisID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "NemesisID" });
            AlterColumn("dbo.Users", "NemesisID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "NemesisID");
        }
    }
}
