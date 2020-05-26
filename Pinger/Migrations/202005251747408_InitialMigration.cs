namespace Pinger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Site",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Url = c.String(nullable: false, maxLength: 255),
                        Active = c.Boolean(nullable: false),
                        LastPingTime = c.DateTime(),
                        Status = c.String(maxLength: 2000),
                        SameStatus = c.Short(nullable: false),
                        ErrorSent = c.Short(nullable: false),
                        Emails = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteStatus",
                c => new
                    {
                        Id = c.Short(nullable: false),
                        PingTime = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => new { t.Id, t.PingTime })
                .ForeignKey("dbo.Site", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteStatus", "Id", "dbo.Site");
            DropIndex("dbo.SiteStatus", new[] { "Id" });
            DropTable("dbo.SiteStatus");
            DropTable("dbo.Site");
        }
    }
}
