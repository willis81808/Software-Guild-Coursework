namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class featuredcarmerge : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Featureds", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Featureds", new[] { "Car_Id" });
            AddColumn("dbo.Cars", "Featured", c => c.Boolean(nullable: false));
            DropTable("dbo.Featureds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Featureds",
                c => new
                    {
                        FeaturedId = c.Int(nullable: false, identity: true),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FeaturedId);
            
            DropColumn("dbo.Cars", "Featured");
            CreateIndex("dbo.Featureds", "Car_Id");
            AddForeignKey("dbo.Featureds", "Car_Id", "dbo.Cars", "Id");
        }
    }
}
