namespace CarDealership.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bodies",
                c => new
                    {
                        BodyId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.BodyId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Mileage = c.Int(nullable: false),
                        VIN = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MSRP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarColorId = c.Int(nullable: false),
                        InteriorColorId = c.Int(),
                        InteriorId = c.Int(nullable: false),
                        MakeModelId = c.Int(nullable: false),
                        TransmissionId = c.Int(nullable: false),
                        Body_BodyId = c.Int(),
                        MakeModel_ModelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bodies", t => t.Body_BodyId)
                .ForeignKey("dbo.Colors", t => t.CarColorId, cascadeDelete: true)
                .ForeignKey("dbo.Interiors", t => t.InteriorId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.InteriorColorId)
                .ForeignKey("dbo.MakeModels", t => t.MakeModel_ModelId)
                .ForeignKey("dbo.Transmissions", t => t.TransmissionId, cascadeDelete: true)
                .Index(t => t.CarColorId)
                .Index(t => t.InteriorColorId)
                .Index(t => t.InteriorId)
                .Index(t => t.TransmissionId)
                .Index(t => t.Body_BodyId)
                .Index(t => t.MakeModel_ModelId);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ColorId);
            
            CreateTable(
                "dbo.Interiors",
                c => new
                    {
                        InteriorId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.InteriorId);
            
            CreateTable(
                "dbo.MakeModels",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        MakeId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.Makes", t => t.MakeId)
                .Index(t => t.MakeId);
            
            CreateTable(
                "dbo.Makes",
                c => new
                    {
                        MakeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MakeId);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        TransmissionId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.TransmissionId);
            
            CreateTable(
                "dbo.Featureds",
                c => new
                    {
                        FeaturedId = c.Int(nullable: false, identity: true),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.FeaturedId)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Specials",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Car_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SpecialId)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .Index(t => t.Car_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Specials", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Featureds", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.MakeModels", "MakeId", "dbo.Makes");
            DropForeignKey("dbo.Cars", "MakeModel_ModelId", "dbo.MakeModels");
            DropForeignKey("dbo.Cars", "InteriorColorId", "dbo.Colors");
            DropForeignKey("dbo.Cars", "InteriorId", "dbo.Interiors");
            DropForeignKey("dbo.Cars", "CarColorId", "dbo.Colors");
            DropForeignKey("dbo.Cars", "Body_BodyId", "dbo.Bodies");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Specials", new[] { "Car_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Featureds", new[] { "Car_Id" });
            DropIndex("dbo.MakeModels", new[] { "MakeId" });
            DropIndex("dbo.Cars", new[] { "MakeModel_ModelId" });
            DropIndex("dbo.Cars", new[] { "Body_BodyId" });
            DropIndex("dbo.Cars", new[] { "TransmissionId" });
            DropIndex("dbo.Cars", new[] { "InteriorId" });
            DropIndex("dbo.Cars", new[] { "InteriorColorId" });
            DropIndex("dbo.Cars", new[] { "CarColorId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Specials");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Featureds");
            DropTable("dbo.Transmissions");
            DropTable("dbo.Makes");
            DropTable("dbo.MakeModels");
            DropTable("dbo.Interiors");
            DropTable("dbo.Colors");
            DropTable("dbo.Cars");
            DropTable("dbo.Bodies");
        }
    }
}
