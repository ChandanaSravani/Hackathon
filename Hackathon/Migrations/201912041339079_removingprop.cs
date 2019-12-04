namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removingprop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NomineeForms", "Votes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NomineeForms", "Votes");
        }
    }
}
