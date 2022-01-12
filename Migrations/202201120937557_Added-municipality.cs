namespace AutoChek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedmunicipality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Municipalities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Psgc = c.String(),
                        Description = c.String(),
                        Code = c.String(),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Municipalities", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Municipalities", new[] { "ProvinceId" });
            DropTable("dbo.Municipalities");
        }
    }
}
