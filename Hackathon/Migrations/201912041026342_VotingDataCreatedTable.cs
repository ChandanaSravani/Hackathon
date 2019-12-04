namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VotingDataCreatedTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VotingDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlatNo = c.String(),
                        PresidentId = c.Int(nullable: false),
                        VicePresidentId = c.Int(nullable: false),
                        SecretaryId = c.Int(nullable: false),
                        TreasurerId = c.Int(nullable: false),
                        RWAPositionId = c.Int(nullable: false),
                        NomineeFormId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NomineeForms", t => t.NomineeFormId, cascadeDelete: false)
                .ForeignKey("dbo.RWAPositions", t => t.RWAPositionId, cascadeDelete:false)
                .Index(t => t.RWAPositionId)
                .Index(t => t.NomineeFormId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingDatas", "RWAPositionId", "dbo.RWAPositions");
            DropForeignKey("dbo.VotingDatas", "NomineeFormId", "dbo.NomineeForms");
            DropIndex("dbo.VotingDatas", new[] { "NomineeFormId" });
            DropIndex("dbo.VotingDatas", new[] { "RWAPositionId" });
            DropTable("dbo.VotingDatas");
        }
    }
}
