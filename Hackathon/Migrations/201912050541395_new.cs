namespace Hackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlatNumber = c.String(),
                        Name = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BaseDatas");
        }
    }
}
