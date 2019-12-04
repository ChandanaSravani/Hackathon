namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyaddedinnominee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NomineeForms", "FlatNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NomineeForms", "FlatNumber");
        }
    }
}
