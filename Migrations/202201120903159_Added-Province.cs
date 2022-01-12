namespace AutoChek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProvince : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Psgc = c.String(),
                        Description = c.String(),
                        Code = c.String(),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provinces", "RegionId", "dbo.Regions");
            DropIndex("dbo.Provinces", new[] { "RegionId" });
            DropTable("dbo.Provinces");
        }
    }
}
