namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveReferenceKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NomineeForms", "ResidentsOfDreamland_Id", "dbo.ResidentsOfDreamlands");
            DropIndex("dbo.NomineeForms", new[] { "ResidentsOfDreamland_Id" });
            DropColumn("dbo.NomineeForms", "ResidentsId");
            DropColumn("dbo.NomineeForms", "ResidentsOfDreamland_Id");
            DropTable("dbo.ResidentsOfDreamlands");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ResidentsOfDreamlands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlatNumber = c.String(),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.NomineeForms", "ResidentsOfDreamland_Id", c => c.Int());
            AddColumn("dbo.NomineeForms", "ResidentsId", c => c.Int(nullable: false));
            CreateIndex("dbo.NomineeForms", "ResidentsOfDreamland_Id");
            AddForeignKey("dbo.NomineeForms", "ResidentsOfDreamland_Id", "dbo.ResidentsOfDreamlands", "Id");
        }
    }
}
