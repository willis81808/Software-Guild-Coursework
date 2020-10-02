namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vehicledescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Description");
        }
    }
}
