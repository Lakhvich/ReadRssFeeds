namespace ReadRssFeeds.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        PublicDate = c.DateTime(nullable: false),
                        ResurseRSSId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResurseRSSes", t => t.ResurseRSSId, cascadeDelete: true)
                .Index(t => t.ResurseRSSId);
            
            CreateTable(
                "dbo.ResurseRSSes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsItems", "ResurseRSSId", "dbo.ResurseRSSes");
            DropIndex("dbo.NewsItems", new[] { "ResurseRSSId" });
            DropTable("dbo.ResurseRSSes");
            DropTable("dbo.NewsItems");
        }
    }
}
