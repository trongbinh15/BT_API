namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.String(nullable: false, maxLength: 128),
                        CMND = c.String(nullable: false),
                        CustomerName = c.String(nullable: false),
                        RoomID = c.String(),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.String(nullable: false, maxLength: 128),
                        isEnable = c.Boolean(nullable: false),
                        capacity = c.Int(nullable: false),
                        Booking_BookingID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoomID)
                .ForeignKey("dbo.Bookings", t => t.Booking_BookingID)
                .Index(t => t.Booking_BookingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Booking_BookingID", "dbo.Bookings");
            DropIndex("dbo.Rooms", new[] { "Booking_BookingID" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Bookings");
        }
    }
}
