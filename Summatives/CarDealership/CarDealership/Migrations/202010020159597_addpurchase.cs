namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpurchase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        SalesPersonId = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZIP = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseType = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Cars", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "VehicleId", "dbo.Cars");
            DropIndex("dbo.Purchases", new[] { "VehicleId" });
            DropTable("dbo.Purchases");
        }
    }
}
