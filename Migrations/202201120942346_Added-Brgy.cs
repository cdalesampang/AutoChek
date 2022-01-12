
namespace AutoChek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBrgy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barangays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Code = c.String(),
                        MunicipalityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Municipalities", t => t.MunicipalityId, cascadeDelete: true)
                .Index(t => t.MunicipalityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Barangays", "MunicipalityId", "dbo.Municipalities");
            DropIndex("dbo.Barangays", new[] { "MunicipalityId" });
            DropTable("dbo.Barangays");
        }
    }
}
