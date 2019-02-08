namespace ConsoleRSS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RSSNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsName = c.String(),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        NewsURL = c.String(),
                        RSSSourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RSSSources", t => t.RSSSourceId, cascadeDelete: true)
                .Index(t => t.RSSSourceId);
            
            CreateTable(
                "dbo.RSSSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RSSSourceURL = c.String(),
                        SourceName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSSNews", "RSSSourceId", "dbo.RSSSources");
            DropIndex("dbo.RSSNews", new[] { "RSSSourceId" });
            DropTable("dbo.RSSSources");
            DropTable("dbo.RSSNews");
        }
    }
}
