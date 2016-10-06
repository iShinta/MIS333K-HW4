namespace Ho_MinhTri_HW4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeID = c.Int(nullable: false, identity: true),
                        CommitteeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommitteeID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        OKToText = c.Boolean(nullable: false),
                        Major = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventTitle = c.String(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        EventLocation = c.String(nullable: false),
                        CustomersOnly = c.Boolean(nullable: false),
                        SponsoringCommittee_CommitteeID = c.Int(),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Committees", t => t.SponsoringCommittee_CommitteeID)
                .Index(t => t.SponsoringCommittee_CommitteeID);
            
            CreateTable(
                "dbo.CustomerCommittees",
                c => new
                    {
                        Customer_CustomerID = c.Int(nullable: false),
                        Committee_CommitteeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Customer_CustomerID, t.Committee_CommitteeID })
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Committees", t => t.Committee_CommitteeID, cascadeDelete: true)
                .Index(t => t.Customer_CustomerID)
                .Index(t => t.Committee_CommitteeID);
            
            CreateTable(
                "dbo.EventCustomers",
                c => new
                    {
                        Event_EventID = c.Int(nullable: false),
                        Customer_CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventID, t.Customer_CustomerID })
                .ForeignKey("dbo.Events", t => t.Event_EventID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID, cascadeDelete: true)
                .Index(t => t.Event_EventID)
                .Index(t => t.Customer_CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "SponsoringCommittee_CommitteeID", "dbo.Committees");
            DropForeignKey("dbo.EventCustomers", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.EventCustomers", "Event_EventID", "dbo.Events");
            DropForeignKey("dbo.CustomerCommittees", "Committee_CommitteeID", "dbo.Committees");
            DropForeignKey("dbo.CustomerCommittees", "Customer_CustomerID", "dbo.Customers");
            DropIndex("dbo.EventCustomers", new[] { "Customer_CustomerID" });
            DropIndex("dbo.EventCustomers", new[] { "Event_EventID" });
            DropIndex("dbo.CustomerCommittees", new[] { "Committee_CommitteeID" });
            DropIndex("dbo.CustomerCommittees", new[] { "Customer_CustomerID" });
            DropIndex("dbo.Events", new[] { "SponsoringCommittee_CommitteeID" });
            DropTable("dbo.EventCustomers");
            DropTable("dbo.CustomerCommittees");
            DropTable("dbo.Events");
            DropTable("dbo.Customers");
            DropTable("dbo.Committees");
        }
    }
}
