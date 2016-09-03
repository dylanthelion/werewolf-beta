namespace Werewolf_Beta.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 20),
                    FBLoginToken = c.String(),
                    GoogleLoginToken = c.String(),
                    Password = c.String(nullable: false, maxLength: 32),
                    NemesisID = c.Int(),
                    Experience = c.Int(nullable: false),
                    Level = c.Int(nullable: false),
                    Tokens = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.NemesisID)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.NemesisID);
        }
        
        public override void Down()
        {
            
        }
    }
}
