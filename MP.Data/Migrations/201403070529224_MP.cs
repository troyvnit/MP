namespace MP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        SenderName = c.String(),
                        SenderPhone = c.String(),
                        ReceiverName = c.String(),
                        ReceiverPhone = c.String(),
                        DeliveryAddress = c.String(),
                        Town = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DepartureTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        DepartureDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Items", "TripId", "dbo.Trips");
            DropIndex("dbo.Passengers", new[] { "TripId" });
            DropIndex("dbo.Items", new[] { "TripId" });
            DropTable("dbo.Passengers");
            DropTable("dbo.Trips");
            DropTable("dbo.Items");
        }
    }
}
