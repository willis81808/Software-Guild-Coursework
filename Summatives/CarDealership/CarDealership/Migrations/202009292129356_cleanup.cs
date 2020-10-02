namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Body_BodyId", "dbo.Bodies");
            DropForeignKey("dbo.Cars", "MakeModel_ModelId", "dbo.MakeModels");
            DropIndex("dbo.Cars", new[] { "Body_BodyId" });
            DropIndex("dbo.Cars", new[] { "MakeModel_ModelId" });
            RenameColumn(table: "dbo.Cars", name: "Body_BodyId", newName: "BodyId");
            RenameColumn(table: "dbo.Cars", name: "MakeModel_ModelId", newName: "ModelId");
            AlterColumn("dbo.Cars", "BodyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cars", "ModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "BodyId");
            CreateIndex("dbo.Cars", "ModelId");
            AddForeignKey("dbo.Cars", "BodyId", "dbo.Bodies", "BodyId", cascadeDelete: true);
            AddForeignKey("dbo.Cars", "ModelId", "dbo.MakeModels", "ModelId", cascadeDelete: true);
            DropColumn("dbo.Cars", "MakeModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "MakeModelId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cars", "ModelId", "dbo.MakeModels");
            DropForeignKey("dbo.Cars", "BodyId", "dbo.Bodies");
            DropIndex("dbo.Cars", new[] { "ModelId" });
            DropIndex("dbo.Cars", new[] { "BodyId" });
            AlterColumn("dbo.Cars", "ModelId", c => c.Int());
            AlterColumn("dbo.Cars", "BodyId", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "ModelId", newName: "MakeModel_ModelId");
            RenameColumn(table: "dbo.Cars", name: "BodyId", newName: "Body_BodyId");
            CreateIndex("dbo.Cars", "MakeModel_ModelId");
            CreateIndex("dbo.Cars", "Body_BodyId");
            AddForeignKey("dbo.Cars", "MakeModel_ModelId", "dbo.MakeModels", "ModelId");
            AddForeignKey("dbo.Cars", "Body_BodyId", "dbo.Bodies", "BodyId");
        }
    }
}
