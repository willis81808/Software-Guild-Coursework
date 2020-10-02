namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specialsupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Specials", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Specials", new[] { "Car_Id" });
            AddColumn("dbo.Specials", "Title", c => c.String());
            AddColumn("dbo.Specials", "Description", c => c.String());
            DropColumn("dbo.Specials", "Car_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Specials", "Car_Id", c => c.Int());
            DropColumn("dbo.Specials", "Description");
            DropColumn("dbo.Specials", "Title");
            CreateIndex("dbo.Specials", "Car_Id");
            AddForeignKey("dbo.Specials", "Car_Id", "dbo.Cars", "Id");
        }
    }
}
