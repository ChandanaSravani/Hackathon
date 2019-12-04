namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingpropinvotingdata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VotingDatas", "NomineeFormId", "dbo.NomineeForms");
            DropForeignKey("dbo.VotingDatas", "RWAPositionId", "dbo.RWAPositions");
            DropIndex("dbo.VotingDatas", new[] { "RWAPositionId" });
            DropIndex("dbo.VotingDatas", new[] { "NomineeFormId" });
            AlterColumn("dbo.VotingDatas", "VicePresidentId", c => c.Int());
            AlterColumn("dbo.VotingDatas", "SecretaryId", c => c.Int());
            AlterColumn("dbo.VotingDatas", "TreasurerId", c => c.Int());
            DropColumn("dbo.VotingDatas", "RWAPositionId");
            DropColumn("dbo.VotingDatas", "NomineeFormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VotingDatas", "NomineeFormId", c => c.Int(nullable: false));
            AddColumn("dbo.VotingDatas", "RWAPositionId", c => c.Int(nullable: false));
            AlterColumn("dbo.VotingDatas", "TreasurerId", c => c.Int(nullable: false));
            AlterColumn("dbo.VotingDatas", "SecretaryId", c => c.Int(nullable: false));
            AlterColumn("dbo.VotingDatas", "VicePresidentId", c => c.Int(nullable: false));
            CreateIndex("dbo.VotingDatas", "NomineeFormId");
            CreateIndex("dbo.VotingDatas", "RWAPositionId");
            AddForeignKey("dbo.VotingDatas", "RWAPositionId", "dbo.RWAPositions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VotingDatas", "NomineeFormId", "dbo.NomineeForms", "Id", cascadeDelete: true);
        }
    }
}
