namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchasedate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "PurchaseDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "PurchaseDate");
        }
    }
}
