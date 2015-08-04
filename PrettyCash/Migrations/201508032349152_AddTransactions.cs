namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AmountMST = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountCur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(nullable: false),
                        CreatedBy_Id = c.String(maxLength: 128),
                        Currency_Id = c.Guid(),
                        ModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Currency_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ISO = c.String(nullable: false, maxLength: 3),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.Transactions", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Transactions", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Transactions", new[] { "Currency_Id" });
            DropIndex("dbo.Transactions", new[] { "CreatedBy_Id" });
            DropTable("dbo.Currencies");
            DropTable("dbo.Transactions");
        }
    }
}
