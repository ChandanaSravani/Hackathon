namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NomineeForms", "CandidateName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NomineeForms", "CandidateName");
        }
    }
}
