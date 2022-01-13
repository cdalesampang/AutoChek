namespace AutoChek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContactNo = c.String(),
                        Email = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        BrgyId = c.Int(nullable: false),
                        Street = c.String(),
                        UnitNo = c.String(),
                        ZipCode = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Barangays", t => t.BrgyId, cascadeDelete: false)
                .ForeignKey("dbo.Municipalities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: false)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId)
                .Index(t => t.BrgyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shops", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Shops", "CityId", "dbo.Municipalities");
            DropForeignKey("dbo.Shops", "BrgyId", "dbo.Barangays");
            DropIndex("dbo.Shops", new[] { "BrgyId" });
            DropIndex("dbo.Shops", new[] { "CityId" });
            DropIndex("dbo.Shops", new[] { "ProvinceId" });
            DropTable("dbo.Shops");
        }
    }
}
